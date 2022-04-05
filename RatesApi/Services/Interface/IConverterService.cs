using Newtonsoft.Json.Linq;
using RestSharp;

namespace RatesApi.Services.Interface
{
    public interface IConverterService
    {
        Dictionary<string, decimal> ConvertToDictionarySecondSource(JObject json);
        Dictionary<string, decimal> ConvertToDictionaryFirstSource(JObject json);
    }
}