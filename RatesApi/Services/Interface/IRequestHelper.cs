using RestSharp;

namespace RatesApi.Core
{
    public interface IRequestHelper
    {
        Task<RestResponse<T>> SendRequest<T>(string url, string path, string jwtToken = "null");
    }
}