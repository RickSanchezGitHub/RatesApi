using Marvelous.Contracts.ExchangeModels;
using Newtonsoft.Json.Linq;

namespace RatesApi.Services.Interface
{
    public interface IConverterService
    {
        Dictionary<string, decimal> ConvertToDictionarySecondSource(JObject json);
        Dictionary<string, decimal> ConvertToDictionaryFirstSource(JObject json);
    }
}