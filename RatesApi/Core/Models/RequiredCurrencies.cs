using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Core
{
    public class RequiredCurrencies : IRequiredCurrencies
    {
        private readonly string _currencyBase;
        private readonly List<string> _currencies;
        public RequiredCurrencies(IOptions<Settings> options)
        {
            _currencyBase = options.Value.CurrencyBase;
            _currencies = options.Value.Currencies;
        }
        public List<string> GetCurrenciesPairs()
        {
            List<string> currencyPairs = new List<string>();
            foreach (var item in _currencies)
            {
                if (item != _currencyBase)
                {
                    currencyPairs.Add(_currencyBase + item);
                }
            }
            return currencyPairs;
        }
    }
}
