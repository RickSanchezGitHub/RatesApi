using Newtonsoft.Json.Linq;

namespace RatesApi.Services.Interface
{
    public interface IBaseClient
    {
        Task<JObject> GetResponseSourse(string url);
    }
}