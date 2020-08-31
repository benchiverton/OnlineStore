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
                FullPriceGBP = 20m,
                DealPriceGBP = 10m,
                Details = "Free Shipping, Tax included. 50% off!"
            },
            new Pricing
            {
                Id = 10,
                FullPriceGBP = 40m,
                DealPriceGBP = 20m,
                Details = "Free Shipping, Tax included. Still 50% off!"
            },
            new Pricing
            {
                Id = 20,
                FullPriceGBP = 50m,
                DealPriceGBP = 25m,
                Details = "50% off for a limited time!"
            },
            new Pricing
            {
                Id = 30,
                FullPriceGBP = 60m,
                DealPriceGBP = 30m,
                Details = "Free Shipping, Tax included. 50% off for a limited time!"
            },
        };
    }
}
