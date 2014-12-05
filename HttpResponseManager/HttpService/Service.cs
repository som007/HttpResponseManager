using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HttpResponseManager.Interface;

namespace HttpResponseManager.Common
{
    internal class Service : IService
    {
        /// <summary>
        /// Post data to URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <param name="header">The header.</param>
        /// <returns>
        /// Api response
        /// </returns>
        public async Task<string> POST(string url, string content, Dictionary<string, string> header)
        {
            try
            {
                return await WebApiPostResponse(url, content, header);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets String data from URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// URL data
        /// </returns>
        public async Task<string> UrlResponse(string url)
        {
            try
            {
                var result = await GetUrlStringData(url);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the URL Response
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// API Response  
        /// </returns>
        public async Task<string> GET(string url)
        {
            try
            {
                return await WebApiGetResponse(url);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Post data to URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        /// API Response - Success/Failed
        /// </returns>
        public async Task<string> POST(string url, string content)
        {
            try
            {
                return await WebApiPostResponse(url, content);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the URL Response
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// API Response  
        /// </returns>
        public async Task<string> GET(string url, Dictionary<string, string> header)
        {
            try
            {
                return await WebApiGetResponse(url, header);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        internal async Task<string> WebApiPostResponse(string url, string postData)
        {
            try
            {
                var handler = new HttpClientHandler();
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                HttpContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                request.Content = content;
                if (handler.SupportsTransferEncodingChunked())
                {
                    request.Headers.TransferEncodingChunked = true;
                }
                var response = await httpClient.SendAsync(request);
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async Task<string> WebApiGetResponse(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(request);
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async Task<string> WebApiGetResponse(string url, Dictionary<string, string> headers)
        {
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                if (headers != null)
                {
                    foreach (var value in headers)
                    {
                        request.Headers.Add(value.Key, value.Value);
                    }
                }
                var response = await httpClient.SendAsync(request);
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async Task<string> WebApiPostResponse(string url, string postData, Dictionary<string, string> headers)
        {
            try
            {
                var handler = new HttpClientHandler();
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                HttpContent content = new StringContent(postData, Encoding.UTF8, "application/json");
                request.Content = content;

                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }
                }

                if (handler.SupportsTransferEncodingChunked())
                {
                    request.Headers.TransferEncodingChunked = true;
                }

                var response = await httpClient.SendAsync(request);

                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal async Task<string> GetUrlStringData(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(url);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
