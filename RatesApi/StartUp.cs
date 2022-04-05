using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using NLog;
using RatesApi.Core;
using RatesApi.Services;
using RatesApi.Services.Interface;
using System.Reflection;
using System.Timers;
using Microsoft.Extensions.Logging;
using MassTransit;

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
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            _configuration = builder.Build();

            _serviceProvider = new ServiceCollection()
            .Configure<Settings>(_configuration)
            .AddLogging()
            .AddMassTransit()
            .RegistrationService()
            .BuildServiceProvider();

            LogerConfig.ConfigureNlog();

            _logger.Info("The program is started");
        }

        public async Task Start()
        { 
            await _serviceProvider.GetService<IRabbitApiService>().SendMessageRabbitService();           
        }
    }
}
