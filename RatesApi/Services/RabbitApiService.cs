using Marvelous.Contracts.ExchangeModels;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using RatesApi.Core;
using RatesApi.Services.Interface;
using System.Timers;

namespace RatesApi.Services
{
    public class RabbitApiService : IRabbitApiService
    {
        private readonly System.Timers.Timer _timer;
        private readonly IBusControl _busControl;
        private readonly CancellationTokenSource _sourse;
        private readonly ICurrencyRatesService _currencyRatesService;
        private readonly ILogger<RabbitApiService> _logger;
        private Dictionary<string, decimal> _currencyRates;

        public RabbitApiService(ICurrencyRatesService currencyRatesService, IOptions<Settings> options, ILogger<RabbitApiService> logger)
        {
            _logger = logger;
            _currencyRatesService = currencyRatesService;

            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {

            });

            _sourse = new CancellationTokenSource(TimeSpan.FromSeconds(options.Value.TimeOut));

            _timer = new System.Timers.Timer(options.Value.TimerInterval);
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Elapsed += new ElapsedEventHandler(TimerSendMessage);
            _timer.Start();

             _busControl.StartAsync(_sourse.Token);

            _logger.LogInformation("The timer is started");
        }

        public async Task SendMessageRabbitService()
        {
            try
            {
                _currencyRates = await _currencyRatesService.GetDataFromFirstSource();
                await _busControl.Publish<CurrencyRatesExchangeModel>(new
                {
                    Rates = _currencyRates
                });
                _logger.LogDebug("send message on Rabbit");
            }
            catch (Exception ex)
            {
                _logger.LogError("Sending the message failed", ex);
                throw new Exception();
            }
        }

        private async void TimerSendMessage(Object source, ElapsedEventArgs a)
        {
            await SendMessageRabbitService();
        }
    }
}
