using Newtonsoft.Json.Linq;
using RestSharp;

namespace RatesApi.Services.Interface
{
    public interface IBaseClient
    {
        Task<RestResponse> GetResponseSourse(string url);
    }
}