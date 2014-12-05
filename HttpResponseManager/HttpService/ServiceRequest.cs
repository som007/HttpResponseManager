using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpResponseManager.Interface;
using Newtonsoft.Json;

namespace HttpResponseManager.Common
{

    /// <summary>
    /// 
    /// </summary>
    public enum HttpRequestMethod
    {
        Post, Get, URL, PostWithHeaders, GetWithHeaders
    }

    /// <summary>
    /// 
    /// </summary>
    public class ServiceRequest
    {
        private IService _servicecall;

        public ServiceRequest()
            : this(new Service())
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRequest"/> class.
        /// </summary>
        /// <param name="serviceCall">The service call.</param>
        private ServiceRequest(IService serviceCall)
        {
            _servicecall = serviceCall;
        }


        /// <summary>
        /// Gets the URL response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodType">Type of the method.</param>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <param name="header">The header.</param>
        /// <returns></returns>
        public async Task<T> GetUrlResponse<T>(HttpRequestMethod methodType, string url, string data, Dictionary<string, string> header)
        {
            string response = "";
            try
            {
                switch (methodType)
                {
                    case HttpRequestMethod.Get:
                        response = await _servicecall.GET(url);
                        break;
                    case HttpRequestMethod.Post:
                        response = await _servicecall.POST(url, data);
                        break;
                    case HttpRequestMethod.URL:
                        response = await _servicecall.UrlResponse(url);
                        break;
                    case HttpRequestMethod.GetWithHeaders:
                        response = await _servicecall.GET(url, header);
                        break;
                    case HttpRequestMethod.PostWithHeaders:
                        response = await _servicecall.POST(url, data, header);
                        break;
                    default:
                        response = "";
                        break;
                }

                return JsonConvert.DeserializeObject<T>("{" + response + "}");
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }


        /// <summary>
        /// Tests the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public List<T> Test<T>(string data)
        {
            return JsonConvert.DeserializeObject<List<T>>(data);
        }
    }
}
