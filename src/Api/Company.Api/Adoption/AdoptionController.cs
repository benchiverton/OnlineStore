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
    public async Task<IActionResult> GetAdoptableRocks()
    {
        var petRockDtos = _context.RocksUpForAdoption;
        using var activity = MyActivitySource.StartActivity("GetAdoptableRocksCustomActivity");
        var petRocks = await petRockDtos.Select(p => p.FromPetRockDto()).ToListAsync();
        return Ok(petRocks);
    }

    [HttpGet("rocks/{adoptableRockId}")]
    public async Task<IActionResult> GetAdoptableRockById(Guid adoptableRockId)
    {
        var petRockDto = await _context.RocksUpForAdoption.FindAsync(adoptableRockId);
        var petRock = petRockDto.FromPetRockDto();
        return Ok(petRock);
    }

    [HttpGet("rocks/{adoptableRockId}/variants")]
    public async Task<IActionResult> GetAdoptableRockVariants(Guid adoptableRockId)
    {
        var variantDtos = await _context.RockVariantsUpForAdoption.Where(v => v.AdoptableRockId == adoptableRockId)
            .ToListAsync();
        var variants = variantDtos.Select(v => v.FromVariantDto());
        return Ok(variants);
    }

    [HttpPost("rocks/{adoptableRockId}/variants/{variantId}/adopt")]
    public async Task<IActionResult> AdoptRock(Guid adoptableRockId, Guid variantId, [FromQuery] string name)
    {
        var adoptableRockDto = await _context.RocksUpForAdoption.FindAsync(adoptableRockId)!;
        var variantDto = await _context.RockVariantsUpForAdoption.FindAsync(variantId);

        var petRock = new PetRockDto
        {
            Owner = Guid.NewGuid(),// TODO
            Name = name,
            Catchphrase = adoptableRockDto!.Catchphrase,
            Attributes = variantDto!.VariantTypeValues,
            Images = adoptableRockDto!.Images
        };
        await _context.PetRocks.AddAsync(petRock);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("petrocks")]
    public async Task<IActionResult> GetMyPets()
    {
        var owner = Guid.NewGuid(); // TODO
        var petRockDtos = await _context.PetRocks.Where(v => v.Owner == owner)
            .ToListAsync();
        var petRocks = petRockDtos.Select(v => v.FromPetRockDto());
        return Ok(petRockDtos);
    }
}
