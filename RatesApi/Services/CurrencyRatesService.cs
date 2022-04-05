using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using NLog;
using RatesApi.Core;
using RatesApi.Services.Interface;

namespace RatesApi.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IConverterService _converterService;
        private readonly string _firstServiceUrl;
        private readonly string _secondServiceUrl;
        private readonly string _currencyBase;
        private readonly IBaseClient _baseClient;

        public CurrencyRatesService(IOptions<Settings> options, IConverterService converterService, IBaseClient baseClient)
        {
            _converterService = converterService;
            _baseClient = baseClient;
            _firstServiceUrl = options.Value.UrlFirstService;
            _secondServiceUrl = options.Value.UrlSecondService;
            _currencyBase = options.Value.CurrencyBase;
        }

        public async Task<Dictionary<string, decimal>> GetDataFromFirstSource()
        {
            var json = await _baseClient.GetResponseSourse(_firstServiceUrl);
            var currencies = await CheckJson(json);
            return currencies;
        }

        public async Task<Dictionary<string, decimal>> GetDataFromSecondSource()
        {
            var json = await _baseClient.GetResponseSourse(_secondServiceUrl);
            var currencies = await CheckJson(json);
            return currencies;
        }

        private async Task<Dictionary<string, decimal>> CheckJson(JObject json)
        {
            var currencies = new Dictionary<string, decimal>();

            if (json != null)
            {
                _logger.Debug("Received a responce from the sourse");
                currencies = _converterService.ConvertToDictionaryFirstSource(json);
            }
            else
            {
                _logger.Error("Could not get data from sources");
            }

            return currencies;
        }
    }
}
