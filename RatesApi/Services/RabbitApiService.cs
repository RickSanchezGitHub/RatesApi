using Marvelous.Contracts.ExchangeModels;
using MassTransit;
using Microsoft.Extensions.Logging;
using NLog;
using RatesApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Services
{
    public class RabbitApiService : IRabbitApiService
    {
        private const int timeOut = 5000;
        private Dictionary<string, decimal> _currencyRates;
        private ICurrencyRatesService _currencyRatesService;
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public RabbitApiService(ICurrencyRatesService currencyRatesService)
        {
            _currencyRatesService = currencyRatesService;
        }
        public async Task SendMessageRabbitService()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("rabbitmq://80.78.240.16", h =>
                {
                    h.Username("nafanya");
                    h.Password("qwe!23");
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);

            try
            {
                if (_currencyRatesService.GetDataFromFirstSource().Wait(timeOut))
                {
                    _currencyRates = await _currencyRatesService.GetDataFromFirstSource();
                }
                else
                {
                    _currencyRates = await _currencyRatesService.GetDataFromSecondSource();
                }

                await busControl.Publish<ICurrencyRatesExchangeModel>(new
                {
                    Rates = _currencyRates
                });

                _logger.Debug("send message");
            }
            catch (Exception ex)
            {
                _logger.Error("Sending the message failed", ex);
                throw new Exception();
            }
            finally
            {
                await busControl.StopAsync();
            }
        }        
    }
}
