using System.Collections.Generic;
using System.Linq;
using Company.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Company.Api.Products;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger) => _logger = logger;

    [HttpGet("")]
    public IActionResult GetProducts() => Ok(_products.Values);

    [HttpGet("{productId}")]
    public IActionResult GetProductById(string productId) => Ok(_products[productId]);

    [HttpGet("{productId}/variants")]
    public IActionResult GetProductVariants(string productId) => Ok(_productVariants[productId]);

    [HttpGet("{productId}/variants/{variantId}")]
    public IActionResult GetProductVariantById(string productId, string variantId) => Ok(_productVariants[productId].First(pv => pv.VariantId == variantId));

    private readonly Dictionary<string, Product> _products = new Dictionary<string, Product> {
        {
            "the_product_one",
            new Product(
                "the_product_one",
                "The Product One",
                "A great product for great people",
                "Improve your well-being with Product One",
                new List<string>{ "Size", "Colour" },
                new List<string> { @"images\products\the_product_one.png" }
            )
        },
        {
            "the_product_two",
            new Product(
                "the_product_two",
                "The Product Two",
                "A brilliant product for brilliant people",
                "Improve your mindfulness with Product Two",
                new List<string>{ "Magnitude", "Emotion" },
                new List<string>{ @"images\products\the_product_two.png" }
            )
        }
    };

    private readonly Dictionary<string, List<Variant>> _productVariants = new Dictionary<string, List<Variant>>
    {
        {
            "the_product_one",
            new List<Variant>
            {
                new Variant(
                    "the_product_one",
                    "0",
                    new Dictionary<string, string> {
                        { "Size", "Small" },
                        { "Colour", "Green" },
                    }
                ),
                new Variant(
                    "the_product_one",
                    "1",
                    new Dictionary<string, string> {
                        { "Size", "Small" },
                        { "Colour", "Greener" },
                    }
                ),
                new Variant(
                    "the_product_one",
                    "2",
                    new Dictionary<string, string> {
                        { "Size", "Small" },
                        { "Colour", "Greenest" },
                    }
                ),
                new Variant(
                    "the_product_one",
                    "10",
                    new Dictionary<string, string> {
                        { "Size", "Big" },
                        { "Colour", "Green" },
                    }
                ),
                new Variant(
                    "the_product_one",
                    "11",
                    new Dictionary<string, string> {
                        { "Size", "Big" },
                        { "Colour", "Greener" },
                    }
                ),
                new Variant(
                    "the_product_one",
                    "12",
                    new Dictionary<string, string> {
                        { "Size", "Big" },
                        { "Colour", "Greenest" },
                    }
                )
            }
        },
        {
            "the_product_two",
            new List<Variant>
            {
                new Variant(
                    "the_product_two",
                    "20",
                    new Dictionary<string, string> {
                        { "Magnitude", "Small" },
                        { "Emotion", "Excited" },
                    }
                ),
                new Variant(
                    "the_product_two",
                    "21",
                    new Dictionary<string, string> {
                        { "Magnitude", "Small" },
                        { "Emotion", "Happy" },
                    }
                ),
                new Variant(
                    "the_product_two",
                    "22",
                    new Dictionary<string, string> {
                        { "Magnitude", "Small" },
                        { "Emotion", "Flattered" },
                    }
                ),
                new Variant(
                    "the_product_two",
                    "30",
                    new Dictionary<string, string> {
                        { "Magnitude", "Vast" },
                        { "Emotion", "Excited" },
                    }
                ),
                new Variant(
                    "the_product_two",
                    "31",
                    new Dictionary<string, string> {
                        { "Magnitude", "Vast" },
                        { "Emotion", "Happy" },
                    }
                ),
                new Variant(
                    "the_product_two",
                    "32",
                    new Dictionary<string, string> {
                        { "Magnitude", "Vast" },
                        { "Emotion", "Flattered" },
                    }
                )
            }
        }
    };
}
