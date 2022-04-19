using Newtonsoft.Json.Linq;

namespace RatesApi.Services.Interface
{
    public interface ICurrencyRatesService
    {
        Task<Dictionary<string, decimal>> ValidCurrensySourse();
        Task<Dictionary<string, decimal>> GetDataFromFirstSource(JObject json);
        Task<Dictionary<string, decimal>> GetDataFromSecondSource(JObject json);
    }
}