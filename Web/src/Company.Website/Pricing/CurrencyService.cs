using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Company.Website.Pricing
{
    public class CurrencyService
    {
        private readonly Dictionary<string, string> _currencySymbols = new Dictionary<string, string>
        {
            { "GBP", "£" }
        };

        // TODO
        public async Task<decimal> ConvertCurrency(string currencyCode, decimal amount) => amount;

        public string GetCurrencyCharacter(string currencyCode) => _currencySymbols[currencyCode];
    }
}
