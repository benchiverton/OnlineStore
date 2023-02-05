using System.Collections.Generic;
using System.Linq;
using Company.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Pricing;

[ApiController]
[Route("pricing")]
public class PricingController : ControllerBase
{
    [HttpGet("{productVariantId}")]
    public IActionResult GetProducts(int productVariantId) => Ok(_pricings.First(p => p.Id == _productVariantIdPricingIdMap[productVariantId]));

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

    private readonly List<Price> _pricings = new List<Price>
        {
            new Price
            (
                0,
                20m,
                10m,
                "Free Shipping, Tax included."
            ),
            new Price
            (
                10,
                40m,
                20m,
                "Free Shipping, Tax included."
            ),
            new Price
            (
                20,
                50m,
                25m,
                "Free Shipping, Tax included."
            ),
            new Price
            (
                30,
                60m,
                30m,
                "Free Shipping, Tax included."
            ),
        };
}
