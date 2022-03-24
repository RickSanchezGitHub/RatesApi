using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Core
{
    public static class RequiredCurrencies
    {
        public static List<string> CurrencyPairs = new List<string>
        {
            "USDRUB",
            "USDEUR",
            "USDJPY",
            "USDCNY",
            "USDTRY",
            "USDRSD"
        };
    }
}
