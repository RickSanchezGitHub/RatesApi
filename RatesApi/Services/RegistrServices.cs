using Microsoft.Extensions.DependencyInjection;
using RatesApi.Core;
using RatesApi.Services.Interface;

namespace RatesApi.Services
{
    public static class RegistrServices
    {
        public static IServiceCollection RegistrationService(this IServiceCollection service)
        {
            service.AddSingleton<IBaseClient, BaseClient>()
            .AddSingleton<ICurrencyRatesService, CurrencyRatesService>()
            .AddSingleton<IConverterService, ConverterService>()
            .AddSingleton<IRabbitApiService, RabbitApiService>()
            .AddSingleton<IRequiredCurrencies, RequiredCurrencies>()
            .BuildServiceProvider();

            return service;
        }
    }
}
