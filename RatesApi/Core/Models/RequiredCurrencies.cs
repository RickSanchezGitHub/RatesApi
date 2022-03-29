using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Core
{
    public class RequiredCurrencies
    {        
        private readonly string _currencyBase; 
        public RequiredCurrencies(IOptions<Settings> options)
        {
            _currencyBase = options.Value.CurrencyBase;
        }
        public List<string> GetCurrenciesPairs()
        {
            List<string> currencyPairs = new List<string>();
            foreach (var item in Currencies)
            {
                if(item != _currencyBase)
                {
                    currencyPairs.Add(_currencyBase + item);
                }
            }
            return currencyPairs;
        }
        private static List<string> Currencies = new List<string>
        {
            //"RUB",
            //"USD",
            //"EUR",
            //"JPY",
            //"CNY",
            //"TRY",
            //"RSD",
            $"{Marvelous.Contracts.Enums.Currency.RUB}",
            $"{Marvelous.Contracts.Enums.Currency.USD}",
            $"{Marvelous.Contracts.Enums.Currency.EUR}",
            $"{Marvelous.Contracts.Enums.Currency.JPY}",
            $"{Marvelous.Contracts.Enums.Currency.CNY}",
            $"{Marvelous.Contracts.Enums.Currency.TRY}",
            $"{Marvelous.Contracts.Enums.Currency.RSD}",
        };
    }
}
