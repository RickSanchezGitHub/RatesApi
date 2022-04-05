using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using RatesApi.Services;
using RatesApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Core
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
