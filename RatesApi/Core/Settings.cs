using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatesApi.Core
{
    public class Settings
    {
        public string UrlFirstService { get; set; }
        public string UrlSecondService { get; set; }
        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string CurrencyBase { get; set; }
        public List<string> Currencies { get; set; }
    }
}
