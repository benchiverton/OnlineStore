using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Company.Api.PetRocks.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Company.Api.PetRocks;

[ApiController]
[Route("petrocks")]
public class PetRocksController : ControllerBase
{
    private static readonly ActivitySource MyActivitySource = new(nameof(PetRocksController), "1.0.0");

    private readonly ILogger<PetRocksController> _logger;
    private readonly PetRockContext _context;

    public PetRocksController(ILogger<PetRocksController> logger, PetRockContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetPetRocks() {
        var petRockDtos = _context.PetRocks;
        using var activity = MyActivitySource.StartActivity("GetPetRocksActivity");
        var petRocks = await petRockDtos.Select(p => p.FromPetRockDto()).ToListAsync();
        return Ok(petRocks);
    }

    [HttpGet("{petRockId}")]
    public async Task<IActionResult> GetPetRockById(Guid petRockId)
    {
        var petRockDto = await _context.PetRocks.FindAsync(petRockId);
        var petRock = petRockDto.FromPetRockDto();
        return Ok(petRock);
    }

    [HttpGet("{petRockId}/variants")]
    public async Task<IActionResult> GetPetRockVariants(Guid petRockId)
    {
        var variantDtos = await _context.Variants.Where(v => v.PetRockId == petRockId).ToListAsync();
        var variants = variantDtos.Select(v => v.FromVariantDto());
        return Ok(variants);
    }

    [HttpGet("{petRockId}/variants/{variantId}")]
    public async Task<IActionResult> GetPetRockVariantById(Guid variantId)
    {
        var variantDto = await _context.Variants.FindAsync(variantId);
        var variant = variantDto.FromVariantDto();
        return Ok(variant);
    }
}
