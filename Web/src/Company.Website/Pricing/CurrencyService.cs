using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Company.Website.Pricing
{
    public class CurrencyService
    {
        private readonly Dictionary<string, string> _currencySymbols;

        public CurrencyService()
        {
            _currencySymbols = new Dictionary<string, string>();

            foreach (var ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                try
                {
                    var ri = new RegionInfo(ci.Name);
                    _currencySymbols[ri.ISOCurrencySymbol] = ri.CurrencySymbol;
                }
                catch { }
            }
        }

        public async Task<decimal> ConvertCurrency(string currencyCode, decimal amount)
        {
            return amount;
        }

        public string GetCurrencyCharacter(string currencyCode) => _currencySymbols[currencyCode];
    }
}
