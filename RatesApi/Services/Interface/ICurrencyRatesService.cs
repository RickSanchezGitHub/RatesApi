using Marvelous.Contracts.ExchangeModels;

namespace RatesApi.Services.Interface
{
    public interface ICurrencyRatesService
    {
        Task<Dictionary<string, decimal>> ValidCurrensySourse();
    }
}