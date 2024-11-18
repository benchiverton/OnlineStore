using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Api.Adoption.Dtos;
using Company.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

namespace Company.Api.Adoption.Endpoints;

internal static class GetMyPetRocksEndpoint
{
    internal static RouteGroupBuilder MapGetMyPetRocksEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("petrocks", GetAsync)
            .RequireAuthorization()
            .RequireScope("access_as_user")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces<List<PetRock>>();

        return group;
    }

    private static async Task<IResult> GetAsync(IHttpContextAccessor contextAccessor, AdoptionContext context)
    {
        var userId = contextAccessor.HttpContext?.User.GetObjectId();
        if (string.IsNullOrEmpty(userId))
        {
            return TypedResults.Unauthorized();
        }

        var petRockDtos = await context.PetRocks
            .Where(v => v.OwnerId == userId)
            .ToListAsync();
        var petRocks = petRockDtos.Select(v => v.FromPetRockDto()).ToList();

        return Results.Ok(petRocks);
    }
}
