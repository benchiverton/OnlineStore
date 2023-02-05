using System.Collections.Generic;
using Company.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Company.Api.ProductInfo;

[ApiController]
[Route("productinformation")]
public class ProductInformationController : ControllerBase
{
    private readonly ILogger<ProductInformationController> _logger;

    public ProductInformationController(ILogger<ProductInformationController> logger) => _logger = logger;

    [HttpGet("{productId}")]
    public IActionResult GetProductInformationById(string productId) => _productInformation.TryGetValue(productId, out var productInfo) ? Ok(productInfo) : NotFound();

    private readonly Dictionary<string, ProductInformation> _productInformation = new Dictionary<string, ProductInformation>
        {
            {
                "the_product_one",
                new ProductInformation
                (
                    "the_product_one",
                    "The Product One",
                    "A great product for great people",
                    "Improve your well-being with Product One",
                    new List<string> { @"images\products\the_product_one.png" },
                    new List<string>()
                )
            },
            {
                "the_product_two",
                new ProductInformation
                (
                    "the_product_two",
                    "The Product Two",
                    "A brilliant product for brilliant people",
                    "Improve your mindfulness with Product Two",
                    new List<string>{ @"images\products\the_product_two.png" },
                    new List<string>()
                )
            }
        };
}
