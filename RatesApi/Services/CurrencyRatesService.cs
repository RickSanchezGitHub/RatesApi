using Marvelous.Contracts.ExchangeModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Polly;
using RatesApi.Core;
using RatesApi.Services.Interface;
using RestSharp;

namespace RatesApi.Services
{
    public class CurrencyRatesService : ICurrencyRatesService
    {
        private ILogger<CurrencyRatesService> _logger;
        private readonly IConverterService _converterService;
        private readonly string _firstServiceUrl;
        private readonly string _secondServiceUrl;
        private readonly string _currencyBase;
        private readonly IBaseClient _baseClient;

        public CurrencyRatesService(IOptions<Settings> options, IConverterService converterService, IBaseClient baseClient, ILogger<CurrencyRatesService> logger)
        {
            _logger = logger;
            _converterService = converterService;
            _baseClient = baseClient;
            _firstServiceUrl = options.Value.UrlFirstService;
            _secondServiceUrl = options.Value.UrlSecondService;
            _currencyBase = options.Value.CurrencyBase;
        }

        public async Task<Dictionary<string, decimal>> ValidCurrensySourse()
        {
            var currencies = new Dictionary<string, decimal>();
            var fallBackPolicy = Policy<RestResponse>
                .Handle<Exception>()
                .FallbackAsync(async _ =>
                {
                    _logger.LogError("Could not get a response from the first source");
                    _logger.LogDebug("A request has been sent to get data from second sourse: http://api.currencylayer.com/live?access_key=ec2e67408bda185f0eafae08db506103");
                    var response = await _baseClient.GetResponseSourse("http://api.currencylayer.com/live?access_key=ec2e67408bda185f0eafae08db506103");
                    var json = JObject.Parse(response.Content);
                    currencies = await GetDataFromSecondSource(json);
                    return response;
                });

            var retryPolicy = Policy
                .Handle<Exception>()
                .RetryAsync(3);
            var policy = fallBackPolicy.WrapAsync(retryPolicy);
            var response = await policy.ExecuteAsync(async () => 
            {
                _logger.LogDebug("A request has been sent to get data from first sourse: https://api.currencyapi.com/v3/latest?apikey=Fm5qjdzjiID5C1PsJQEnnrZgpAv3UyjUK1pyxD10");
                var response = await _baseClient.GetResponseSourse("http://api.currencylayer.com/live?access_key=ec2e67408bda185f0eafae08db506103");
                var json = JObject.Parse(response.Content);
                currencies = await GetDataFromSecondSource(json);
                return response;
            });
            return currencies;
        }
        private async Task<Dictionary<string, decimal>> GetDataFromFirstSource(JObject json)
        {
            
            var currencies = new Dictionary<string, decimal>();
            if (json != null)
            {
                _logger.LogDebug("Received a responce from the first sourse: https://api.currencyapi.com/v3/latest?apikey=Fm5qjdzjiID5C1PsJQEnnrZgpAv3UyjUK1pyxD10");
                currencies = _converterService.ConvertToDictionaryFirstSource(json);
                foreach (var item in currencies)
                {
                    _logger.LogDebug($"{item.Key} {item.Value}");
                }
            }
            else
            {
                _logger.LogError("Could not get a response from the first source");
            }
   
            return currencies;
        }

        private async Task<Dictionary<string, decimal>> GetDataFromSecondSource(JObject json)
        {

            var currencies = new Dictionary<string, decimal>();

            if (json != null)
            {
                _logger.LogDebug("Received a responce from the second sourse:  http://api.currencylayer.com/live?access_key=ec2e67408bda185f0eafae08db506103");
                currencies = _converterService.ConvertToDictionarySecondSource(json);
                foreach (var item in currencies)
                {
                    _logger.LogDebug($"{item.Key} {item.Value}");
                }
            }
            else
            {
                _logger.LogError("Could not get data from second sources, an empty library will be sent");
            }

            return currencies;
        }
        
    }
}
