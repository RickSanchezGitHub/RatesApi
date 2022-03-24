using Newtonsoft.Json.Linq;
using RestSharp;

namespace RatesApi.Services.Interface
{
    public interface IConverterService
    {
        Dictionary<string, decimal> ConvertToDictionaryFirstSource(JObject json);
        Dictionary<string, decimal> ConvertToDictionarySecondSource(JObject json);
    }
}