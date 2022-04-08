using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using RatesApi.Core;
using RatesApi.Services;
using RatesApi.Services.Interface;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace RatesApi
{
    public class StartUp
    {
        private IServiceProvider _serviceProvider;
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
            .AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddNLog(_configuration);
            })
            .AddMassTransit()
            .RegistrationService()
            .BuildServiceProvider();
        }

        public async Task Start()
        {
            await _serviceProvider.GetService<IRabbitApiService>().SendMessageRabbitService();
        }
    }
}
