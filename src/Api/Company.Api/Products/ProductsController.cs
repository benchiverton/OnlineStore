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
    public IActionResult GetProducts() => Ok(_products);

    [HttpGet("{productId}")]
    public IActionResult GetProductById(string productId) => Ok(_products.First(p => p.Id == productId));

    private readonly List<Product> _products = new List<Product>
        {
            new Product("the_product_one"),
            new Product("the_product_two")
        };
}
