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
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
        }

        public async static Task<T> GetObjectAsync<T>(string url, IDictionary<string, string> headers = null)
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
                    string strResult = await response.Content.ReadAsStringAsync();
                    return Json.ToObject<T>(strResult);
                }
            }
        }

        public async static Task<T> PostStringAsync<T>(string url, string postData, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Content = new StringContent(postData, Encoding.UTF8);

                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    string strResult = await response.Content.ReadAsStringAsync();
                    return Json.ToObject<T>(strResult);
                }
            }
        }

        public async static Task<T> PostFromAsync<T>(string url, IDictionary<string, string> dicForm, IDictionary<string, string> headers = null)
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
                    string strResult = await response.Content.ReadAsStringAsync();
                    return Json.ToObject<T>(strResult);
                }
            }
        }

        public async static Task<T> PostJsonAsync<T>(string url, object postModel, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Content = new StringContent(Json.ToJson(postModel), Encoding.UTF8, "application/json");

                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    string strResult = await response.Content.ReadAsStringAsync();
                    return Json.ToObject<T>(strResult);
                }
            }
        }

        public async static Task<T> PostMultipartFormDataAsync<T>(string url, IDictionary<string, FileInfo> dicFiles, IDictionary<string, string> dicForm = null, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
            using (MultipartFormDataContent content = new MultipartFormDataContent())
            {
                //添加表单内容
                dicForm?.ToList().ForEach(item =>
                {
                    content.Add(new ByteArrayContent(Encoding.UTF8.GetBytes(item.Value)), item.Key);
                });

                //添加多文件内容
                dicFiles.ToList().ForEach(item =>
                {
                    ByteArrayContent bc = new ByteArrayContent(File.ReadAllBytes(item.Value.FullName));
                    content.Add(bc, item.Key, item.Value.Name);
                });

                request.Content = content;

                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    string strResult = await response.Content.ReadAsStringAsync();
                    return Json.ToObject<T>(strResult);
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
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async static Task<T> PutJsonAsync<T>(string url, object postModel, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url))
            {
                request.Content = new StringContent(Json.ToJson(postModel), Encoding.UTF8, "application/json");

                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await HttpClientFactory.Create(hander).SendAsync(request))
                {
                    string strResult = await response.Content.ReadAsStringAsync();
                    return Json.ToObject<T>(strResult);
                }
            }
        }
    }
}
