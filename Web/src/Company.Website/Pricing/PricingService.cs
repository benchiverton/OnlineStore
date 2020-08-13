using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Website.Pricing
{
    public class PricingService
    {
        public async Task<Pricing> GetPricingByProductTypId(int productVarientId)
        {
            return _pricings.First(p => p.Id == _productVarientIdPricingIdMap[productVarientId]);
        }

        private readonly Dictionary<int, int> _productVarientIdPricingIdMap = new Dictionary<int, int>
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
        };

        private readonly List<Pricing> _pricings = new List<Pricing>
        {
            new Pricing
            {
                Id = 0,
                FullPriceGBP = 20m,
                DealPriceGBP = 10m,
                Details = "50% off. Free Shipping, Tax included."
            },
            new Pricing
            {
                Id = 10,
                FullPriceGBP = 40m,
                DealPriceGBP = 20m,
                Details = "50% off. Free Shipping, Tax included."
            },
            new Pricing
            {
                Id = 20,
                FullPriceGBP = 60m,
                DealPriceGBP = 30m,
                Details = "50% off. Free Shipping, Tax included."
            },
        };
    }
}
