using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Company.Api.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Company.Api.Products;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private static readonly ActivitySource MyActivitySource = new(nameof(ProductsController), "1.0.0");

    private readonly ILogger<ProductsController> _logger;
    private readonly ProductContext _context;

    public ProductsController(ILogger<ProductsController> logger, ProductContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetProducts() {
        var productDtos = _context.Products;
        using var activity = MyActivitySource.StartActivity("GetProductsActivity");
        var products = await productDtos.Select(p => p.FromProductDto()).ToListAsync();
        return Ok(products);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        var productDto = await _context.Products.FindAsync(productId);
        var product = productDto.FromProductDto();
        return Ok(product);
    }

    [HttpGet("{productId}/variants")]
    public async Task<IActionResult> GetProductVariants(Guid productId)
    {
        var variantDtos = await _context.Variants.Where(v => v.ProductId == productId).ToListAsync();
        var variants = variantDtos.Select(v => v.FromVariantDto());
        return Ok(variants);
    }

    [HttpGet("{productId}/variants/{variantId}")]
    public async Task<IActionResult> GetProductVariantById(Guid variantId)
    {
        var variantDto = await _context.Variants.FindAsync(variantId);
        var variant = variantDto.FromVariantDto();
        return Ok(variant);
    }
}
