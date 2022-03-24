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
                {"USDRUB", (decimal)103.525047 },
                {"USDEUR", (decimal)0.9029 },
                {"USDJPY", (decimal)118.904013 },
                {"USDCNY", (decimal)6.361596 },
                {"USDTRY", (decimal)14.7871 },
                {"USDRSD", (decimal)106.159878 },
            };
            return testDataFirstSourse;
        }

        public Dictionary<string, decimal> GetSecondOutputData()
        {
            Dictionary<string, decimal> testDataFirstSourse = new Dictionary<string, decimal>()
            {
                {"USDRUB", (decimal)103.12613 },
                {"USDEUR", (decimal)0.90099},
                {"USDJPY", (decimal)118.48888 },
                {"USDCNY", (decimal)6.34711},
                {"USDTRY", (decimal)14.71872 },
                {"USDRSD", (decimal)106.00167 },
            };
            return testDataFirstSourse;
        }
    }
}
