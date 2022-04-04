using Marvelous.Contracts.ExchangeModels;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using RatesApi.Core;
using RatesApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Services
{
    public class RabbitApiService : IRabbitApiService
    {
        private const int timeOut = 5000;
        private readonly string _host;
        private readonly string _userName;
        private readonly string _password;
        private ICurrencyRatesService _currencyRatesService;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private Dictionary<string, decimal> _currencyRates;

        public RabbitApiService(ICurrencyRatesService currencyRatesService, IOptions<Settings> options)
        {
            _currencyRatesService = currencyRatesService;
            _host = options.Value.Host;
            _userName = options.Value.Login;
            _password = options.Value.Password;
        }

        public async Task SendMessageRabbitService()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(_host), h =>
                {
                    h.Username(_userName);
                    h.Password(_password);
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);

            try
            {
                if (!_currencyRatesService.GetDataFromFirstSource().Wait(timeOut))
                {
                    _currencyRates = await _currencyRatesService.GetDataFromSecondSource();
                }
                else
                {
                    _currencyRates = await _currencyRatesService.GetDataFromFirstSource();
                }

                var endpoint = await busControl.GetSendEndpoint(new Uri("rabbitmq://localhost/test"));
                await endpoint.Send<ICurrencyRatesExchangeModel>(new 
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
                busControl.StopAsync();
            }
        }        
    }
}
