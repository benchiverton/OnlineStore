using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Company.Api.Adoption.Dtos;
using Company.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Company.Api.Adoption.Endpoints;

internal static class GetAdoptableRocksEndpoint
{
    private static readonly ActivitySource MyActivitySource = new(nameof(GetAdoptableRocksEndpoint), "1.0.0");

    internal static RouteGroupBuilder MapGetAdoptableRocksEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("rocks", GetAsync)
            .Produces<List<AdoptableRock>>();

        return group;
    }

    private static async Task<IResult> GetAsync(AdoptionContext context)
    {
        var petRockDtos = context.RocksUpForAdoption;
        using var activity = MyActivitySource.StartActivity();
        var petRocks = await petRockDtos.Select(p => p.FromPetRockDto()).ToListAsync();
        return Results.Ok(petRocks);
    }
}
