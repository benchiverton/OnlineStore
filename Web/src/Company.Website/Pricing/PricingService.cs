using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Website.Pricing
{
    // TODO: rework how pricing is modelled
    public class PricingService
    {
        public async Task<Pricing> GetPricingByProductTypeId(string productId, int productVariantId) => _pricings.First(p => p.Id == _productVariantIdPricingIdMap[productVariantId]);

        private readonly Dictionary<int, int> _productVariantIdPricingIdMap = new Dictionary<int, int>
        {
            {0, 0},
            {1, 0},
            {2, 0},
            {10, 10},
            {11, 10},
            {12, 10},
            {20, 20},
            {21, 20},
            {22, 20},
            {30, 30},
            {31, 30},
            {32, 30},
        };

        private readonly List<Pricing> _pricings = new List<Pricing>
        {
            new Pricing
            {
                Id = 0,
                ListPriceGBP = 20m,
                DealPriceGBP = 10m,
                Details = "Free Shipping, Tax included."
            },
            new Pricing
            {
                Id = 10,
                ListPriceGBP = 40m,
                DealPriceGBP = 20m,
                Details = "Free Shipping, Tax included."
            },
            new Pricing
            {
                Id = 20,
                ListPriceGBP = 50m,
                DealPriceGBP = 25m,
                Details = "Free Shipping, Tax included."
            },
            new Pricing
            {
                Id = 30,
                ListPriceGBP = 60m,
                DealPriceGBP = 30m,
                Details = "Free Shipping, Tax included."
            },
        };
    }
}
