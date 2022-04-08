using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NLog;
using RatesApi.Services.Interface;

namespace RatesApi.Services
{
    public class ConverterService : IConverterService
    {
        private readonly ILogger<ConverterService> _logger;
        private readonly IRequiredCurrencies _currencies;
        public ConverterService(IRequiredCurrencies requiredCurrencies, ILogger<ConverterService> logger)
        {
            _logger =logger;
            _currencies = requiredCurrencies;
        }

        public Dictionary<string, decimal> ConvertToDictionaryFirstSource(JObject json)
        {
            try
            {
                var val = json["data"].Children();

                var result = val.Select(s => new
                {
                    CurrencyName = s.Children().Values("code").FirstOrDefault().ToString(),
                    CurrencyValue = s.Children().Values("value").FirstOrDefault()
                }).ToDictionary(k => "USD" + k.CurrencyName, k => Convert.ToDecimal(k.CurrencyValue));

                _logger.LogDebug("Successful conversion of the main sourse");
                var currencys = SelectionCurrency(result);
                return currencys;
            }
            catch (Exception ex)
            {
                _logger.LogError("an incorrect json file was passed from the main source", ex);
                throw new NullReferenceException("Object reference not set to an instance of an object.");
            }
        }

        public Dictionary<string, decimal> ConvertToDictionarySecondSource(JObject json)
        {
            try
            {
                var val = json["quotes"].Children();
                var result = val.Select(s => new
                {
                    CurrencyName = (s as JProperty).Name,
                    CurrencyValue = (s as JProperty).Value
                }).ToDictionary(k => k.CurrencyName, v => Convert.ToDecimal(v.CurrencyValue));

                var currencys = SelectionCurrency(result);
                _logger.LogDebug("Successful conversion of the secondary sourse");
                return currencys;
            }
            catch (Exception ex)
            {
                _logger.LogError("an incorrect json file was passed from the secondary source", ex);
                throw new NullReferenceException("Object reference not set to an instance of an object.");
            }
        }

        private Dictionary<string, decimal> SelectionCurrency(Dictionary<string, decimal> currencys)
        {
            var currencyPairs = _currencies.GetCurrenciesPairs();
            var reqCurrencies = new Dictionary<string, decimal>();

            foreach (var reqCurrency in currencyPairs)
            {
                foreach (var currency in currencys)
                {
                    if (currency.Key == reqCurrency)
                    {
                        reqCurrencies.Add(currency.Key, Decimal.Round(currency.Value, 2));
                    }
                }
            }
            _logger.LogDebug("The final list of currency pairs has been obtained");
            return reqCurrencies;
        }
    }
}