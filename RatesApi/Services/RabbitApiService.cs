using Marvelous.Contracts.ExchangeModels;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Services
{
    public class RabbitApiService : IRabbitApiService
    {
        private CurrencyRatesService _currencyRatesService;
        public async Task M()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(chg =>
            {
                chg.Host("rabbitmq://80.78.240.16", hst =>
                {
                    hst.Username("nafanya");
                    hst.Password("qwe!23");
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);

            try
            {
                Dictionary<string, decimal>? value = await _currencyRatesService.GetDataFromFirstSource();
                await busControl.Publish<CurrencyRatesExchangeModel>(new CurrencyRatesExchangeModel
                {
                    Rates = value
                });
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
