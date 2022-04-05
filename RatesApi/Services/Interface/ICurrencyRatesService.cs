using Newtonsoft.Json.Linq;

namespace RatesApi.Services.Interface
{
    public interface ICurrencyRatesService
    {
        Task<Dictionary<string, decimal>> GetDataFromFirstSource();
        Task<Dictionary<string, decimal>> GetDataFromSecondSource();
    }
}