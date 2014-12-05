using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpResponseManager.Interface
{
    public interface IService
    {
        Task<string> POST(string url, string content, Dictionary<string, string> header);
        Task<string> UrlResponse(string url);
        Task<string> GET(string url);
        Task<string> POST(string url, string content);
        Task<string> GET(string url, Dictionary<string, string> header);
    }
}
