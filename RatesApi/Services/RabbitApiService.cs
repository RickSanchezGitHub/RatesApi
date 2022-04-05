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
using System.Timers;

namespace RatesApi.Services
{
    public class RabbitApiService : IRabbitApiService
    {
        private readonly System.Timers.Timer _timer;
        private readonly IBusControl _busControl;
        private readonly CancellationTokenSource _sourse;
        private ICurrencyRatesService _currencyRatesService;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private Dictionary<string, decimal> _currencyRates;

        public RabbitApiService(ICurrencyRatesService currencyRatesService, IOptions<Settings> options)
        {
            _currencyRatesService = currencyRatesService;

            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(options.Value.Host), h =>
                {
                    h.Username(options.Value.Login);
                    h.Password(options.Value.Password);
                });
            });
            _sourse = new CancellationTokenSource(TimeSpan.FromSeconds(options.Value.TimeOut));

            _timer = new System.Timers.Timer(options.Value.TimerInterval);
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Elapsed += new ElapsedEventHandler(SendMessage);
            _timer.Start();

            _logger.Info("The timer is started");
        }

        public async Task SendMessageRabbitService()
        {
            await _busControl.StartAsync(_sourse.Token);

            try
            {
                _currencyRates = await _currencyRatesService.GetDataFromFirstSource();               

                await _busControl.Publish<ICurrencyRatesExchangeModel>(new
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
                _busControl.StopAsync();
            }
        }

        private async void SendMessage(Object source, ElapsedEventArgs a)
        {
            await SendMessageRabbitService();
        }
    }
}
