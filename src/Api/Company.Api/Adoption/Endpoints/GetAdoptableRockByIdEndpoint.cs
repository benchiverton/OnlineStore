using System;
using System.Threading.Tasks;
using Company.Api.Adoption.Dtos;
using Company.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Company.Api.Adoption.Endpoints;

internal static class GetAdoptableRockByIdEndpoint
{
    internal static RouteGroupBuilder MapGetAdoptableRockByIdEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("rocks/{adoptableRockId:guid}", GetAsync)
            .Produces<AdoptableRock>();

        return group;
    }

    private static async Task<IResult> GetAsync(Guid adoptableRockId, AdoptionContext context)
    {
        var petRockDto = await context.RocksUpForAdoption.FindAsync(adoptableRockId);
        var petRock = petRockDto.FromPetRockDto();
        return TypedResults.Ok(petRock);
    }
}
