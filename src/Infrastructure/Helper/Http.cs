﻿using System;
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
        private static readonly HttpClient httpClient = null;
        static Http()
        {
            HttpClientHandler hander = new HttpClientHandler()
            {
                UseProxy = false,//禁用代理
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,//启用响应内容压缩
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,//设置访问https url
            };
            httpClient = new HttpClient(hander);
            //httpClient.Timeout = TimeSpan.FromSeconds(10);//超时设置
        }

        public async static Task<string> GetStringAsync(string url, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await httpClient.SendAsync(request))
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

                using (HttpResponseMessage response = await httpClient.SendAsync(request))
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

                using (HttpResponseMessage response = await httpClient.SendAsync(request))
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

                using (HttpResponseMessage response = await httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async static Task<byte[]> PostJsonAsByteArrayAsync(string url, string json, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                //添加http请求头
                headers?.ToList().ForEach(item =>
                {
                    request.Headers.Add(item.Key, item.Value);
                });

                using (HttpResponseMessage response = await httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
        }

        public async static Task<string> PostMultipartFormDataAsync(string url, IDictionary<string, string> dicForm, IDictionary<string, FileSimpleInfo> dicFiles, IDictionary<string, string> headers = null)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url))
            using (MultipartFormDataContent content = new MultipartFormDataContent())
            {
                //添加表单内容
                dicForm.ToList().ForEach(item =>
                {
                    content.Add(new ByteArrayContent(Encoding.UTF8.GetBytes(item.Value)), item.Key);
                });

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

                using (HttpResponseMessage response = await httpClient.SendAsync(request))
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

                using (HttpResponseMessage response = await httpClient.SendAsync(request))
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

                using (HttpResponseMessage response = await httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
