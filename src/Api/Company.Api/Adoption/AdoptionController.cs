using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Company.Api.Adoption.Dtos;
using Company.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace Company.Api.Adoption;

[ApiController]
[Route("adoption")]
public class AdoptionController : ControllerBase
{
    private static readonly ActivitySource MyActivitySource = new(nameof(AdoptionController), "1.0.0");

    private readonly ILogger<AdoptionController> _logger;
    private readonly AdoptionContext _context;
    private readonly IHttpContextAccessor _contextAccessor;

    public AdoptionController(ILogger<AdoptionController> logger, AdoptionContext context,
        IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _context = context;
        _contextAccessor = contextAccessor;
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

    [HttpPost("rocks/adopt")]
    [Authorize]
    [RequiredScope(RequiredScopesConfigurationKey = "access_as_user")]
    public async Task<IActionResult> AdoptRock([FromBody] AdoptRockRequest request)
    {
        var userId = _contextAccessor.HttpContext?.User.GetObjectId();
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var petRock = new PetRockDto
        {
            OwnerId = userId,
            Name = request.Name,
            Catchphrase = request.Catchphrase,
            Attributes = request.Attributes,
            Images = request.Images
        };
        await _context.PetRocks.AddAsync(petRock);
        await _context.SaveChangesAsync();

        return Ok(petRock.Id);
    }

    [HttpGet("petrocks")]
    public async Task<IActionResult> GetMyPets()
    {
        var userId = _contextAccessor.HttpContext?.User.GetObjectId();
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var petRockDtos = await _context.PetRocks
            .Where(v => v.OwnerId == userId)
            .ToListAsync();
        var petRocks = petRockDtos.Select(v => v.FromPetRockDto());

        return Ok(petRocks);
    }
}
