using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using RatesApi.Core;
using RatesApi.Services.Interface;
using RestSharp;
using System.Net;
using System.Text.Json.Nodes;

namespace RatesApi.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IConverterService _converterService;
        private readonly string _firstServiceUrl;
        private readonly string _secondServiceUrl;
        private readonly IBaseClient _baseClient;
        

        public CurrencyRatesService(IOptions<Settings> options, IConverterService converterService, IBaseClient baseClient)
        {
            _converterService = converterService;
            _baseClient = baseClient;
            _firstServiceUrl = options.Value.UrlFirstService;
            _secondServiceUrl = options.Value.UrlSecondService;
        }

        public async Task<Dictionary<string, decimal>> GetDataFromFirstSource()
        {
            var json = await _baseClient.GetResponseSourse(_firstServiceUrl);
            _logger.Debug("ответ из первого источника получен");
            var currencies = _converterService.ConvertToDictionaryFirstSource(json);                
            foreach (var cyrrency in currencies)
            {
               _logger.Info($"{cyrrency.Key} {cyrrency.Value.ToString()}");                
            }
            return currencies;
        }

        public async Task<Dictionary<string, decimal>> GetDataFromSecondSource()
        {
            var json = await _baseClient.GetResponseSourse(_secondServiceUrl);                
            _logger.Debug("ответ со второного источника получен");
            var currencies = _converterService.ConvertToDictionarySecondSource(json);
            foreach (var cyrrency in currencies)
            {
                _logger.Info($"{cyrrency.Key} {cyrrency.Value.ToString()}");
            }
            return currencies;
        }
    }
}
