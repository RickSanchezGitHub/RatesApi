using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvelous.Contracts.ExchangeModels
{
    public interface ICurrencyRatesExchangeModel
    {
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
