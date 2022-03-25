using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Tests.TestSourse
{
    public class CurrencyRatesServiceTestSourse
    {
        public Dictionary<string, decimal> GetFirstOutputData()
        {
            Dictionary<string, decimal> testDataFirstSourse = new Dictionary<string, decimal>()
            {
                 {"USDRUB", (decimal)103.5250 },
                {"USDEUR", (decimal)0.9029 },
                {"USDJPY", (decimal)118.9040 },
                {"USDCNY", (decimal)6.3616 },
                {"USDTRY", (decimal)14.7871 },
                {"USDRSD", (decimal)106.1599 },
            };
            return testDataFirstSourse;
        }

        public Dictionary<string, decimal> GetSecondOutputData()
        {
            Dictionary<string, decimal> testDataFirstSourse = new Dictionary<string, decimal>()
            {
               {"USDRUB", (decimal)103.1261 },
                {"USDEUR", (decimal)0.9010},
                {"USDJPY", (decimal)118.4889 },
                {"USDCNY", (decimal)6.3471},
                {"USDTRY", (decimal)14.7187 },
                {"USDRSD", (decimal)106.0017 },
            };
            return testDataFirstSourse;
        }
    }
}
