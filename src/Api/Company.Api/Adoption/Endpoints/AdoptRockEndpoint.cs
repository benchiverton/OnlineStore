using System;
using System.Threading.Tasks;
using Company.Api.Adoption.Dtos;
using Company.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Identity.Web;

namespace Company.Api.Adoption.Endpoints;

internal static class AdoptRockEndpoint
{
    internal static RouteGroupBuilder MapAdoptRockEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("rocks/adopt", PostAsync)
            .RequireAuthorization()
            .RequireScope("access_as_user")
            .Accepts<AdoptRockRequest>("application/json")
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces<Guid>();

        return group;
    }

    private static async Task<IResult> PostAsync(AdoptRockRequest request, IHttpContextAccessor contextAccessor, AdoptionContext context)
    {
        var userId = contextAccessor.HttpContext?.User.GetObjectId();
        if (string.IsNullOrEmpty(userId))
        {
            return TypedResults.Unauthorized();
        }

        var petRock = new PetRockDto
        {
            OwnerId = userId,
            Name = request.Name,
            Catchphrase = request.Catchphrase,
            Attributes = request.Attributes,
            Images = request.Images
        };
        await context.PetRocks.AddAsync(petRock);
        await context.SaveChangesAsync();

        return TypedResults.Ok(petRock.Id);
    }
}
