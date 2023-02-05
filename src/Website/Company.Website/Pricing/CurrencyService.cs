using System.Collections.Generic;

namespace Company.Website.Pricing;

public class CurrencyService
{
    private readonly Dictionary<string, string> _currencySymbols = new Dictionary<string, string>
        {
            { "GBP", "£" }
        };

    public string GetCurrencyCharacter(string currencyCode) => _currencySymbols[currencyCode];
}
