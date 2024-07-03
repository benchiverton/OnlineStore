using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Company.Api.Adoption.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Company.Api.Adoption;

[ApiController]
[Route("adoption")]
public class AdoptionController : ControllerBase
{
    private static readonly ActivitySource MyActivitySource = new(nameof(AdoptionController), "1.0.0");

    private readonly ILogger<AdoptionController> _logger;
    private readonly AdoptionContext _context;

    public AdoptionController(ILogger<AdoptionController> logger, AdoptionContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("rocks")]
    public async Task<IActionResult> GetRocks() {
        var petRockDtos = _context.RocksUpForAdoption;
        using var activity = MyActivitySource.StartActivity("GetPetRocksActivity");
        var petRocks = await petRockDtos.Select(p => p.FromPetRockDto()).ToListAsync();
        return Ok(petRocks);
    }

    [HttpGet("rocks/{petRockId}")]
    public async Task<IActionResult> GetRockById(Guid petRockId)
    {
        var petRockDto = await _context.RocksUpForAdoption.FindAsync(petRockId);
        var petRock = petRockDto.FromPetRockDto();
        return Ok(petRock);
    }

    [HttpGet("rocks/{petRockId}/variants")]
    public async Task<IActionResult> GetRockVariants(Guid petRockId)
    {
        var variantDtos = await _context.RockVariantsUpForAdoption.Where(v => v.PetRockId == petRockId).ToListAsync();
        var variants = variantDtos.Select(v => v.FromVariantDto());
        return Ok(variants);
    }

    [HttpGet("rocks/{petRockId}/variants/{variantId}")]
    public async Task<IActionResult> GetRockVariantById(Guid variantId)
    {
        var variantDto = await _context.RockVariantsUpForAdoption.FindAsync(variantId);
        var variant = variantDto.FromVariantDto();
        return Ok(variant);
    }
}
