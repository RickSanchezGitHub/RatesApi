using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatesApi.Core;
using RatesApi.Services.Interface;
using System.Configuration;

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
            .AddSingleton<IRequestHelper, RequestHelper>()
            .AddTransient<IInitializeHelper, InitializeHelper>();

            return service;
        }
    }
}
