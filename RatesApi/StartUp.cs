using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using RatesApi.Core;
using RatesApi.Services;
using RatesApi.Services.Interface;
using System.Reflection;
using System.Timers;

namespace RatesApi
{
    public class StartUp
    {
        private IServiceProvider _serviceProvider;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private IConfiguration _configuration;

        public StartUp()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();
                

            _serviceProvider = new ServiceCollection()
            .Configure<Settings>(_configuration)
            .AddLogging()            
            .AddSingleton<IBaseClient, BaseClient>()
            .AddSingleton<ICurrencyRatesService, CurrencyRatesService>()
            .AddSingleton<IConverterService, ConverterService>()
            .AddSingleton<IRabbitApiService, RabbitApiService>()
            .BuildServiceProvider();
            LogerConfig.ConfigureNlog();
            _logger.Info("The programm is started");
        }
        public async Task Start()
        {           
            await _serviceProvider.GetService<IRabbitApiService>().SendMessageRabbitService();

            var Timer = new System.Timers.Timer(30000);

            Timer.AutoReset = true;
            Timer.Enabled = true;

            Timer.Elapsed += new ElapsedEventHandler(SendMessage);

            Timer.Start();
            _logger.Info("The timer is started");
        }

        private async void SendMessage(Object source, ElapsedEventArgs a)
        {
            await _serviceProvider.GetService<IRabbitApiService>().SendMessageRabbitService();
        }
    }
}
