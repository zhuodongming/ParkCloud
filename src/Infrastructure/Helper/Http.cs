using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    /// <summary>
    /// Http Helper
    /// </summary>
    public sealed class Http
    {
        private static HttpClientHandler hander = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,//启用响应内容压缩
            ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,//设置访问https url
        };

        public async static Task<string> GetStringAsync(string url, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async static Task<byte[]> GetBytesAsync(string url, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
        }

        public async static Task<string> PostFromAsync(string url, IDictionary<string, string> dicForm, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Content = new FormUrlEncodedContent(dicForm);

                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async static Task<string> PostJsonAsync(string url, string json, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async static Task<string> PostMultipartFormDataAsync(string url, IDictionary<string, string> dicForm, IDictionary<string, FileSimpleInfo> dicFiles, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
            using (MultipartFormDataContent content = new MultipartFormDataContent())
            {
                //添加表单内容
                content.Add(new FormUrlEncodedContent(dicForm));

                //添加多文件内容
                dicFiles.ToList().ForEach(item =>
                {
                    content.Add(new ByteArrayContent(item.Value.Content), item.Key, item.Value.FileName);
                });

                request.Content = content;

                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async static Task<string> PutJsonAsync(string url, string json, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url))
            {
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async static Task<string> DeleteAsync(string url, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url))
            {
                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
