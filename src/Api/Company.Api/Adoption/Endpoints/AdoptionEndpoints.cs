using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Company.Api.Adoption.Endpoints;

public static class AdoptionEndpoints
{
    public static WebApplication MapAdoptionEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("adoption")
            .WithTags("Adoption");

        endpoints
            .MapAdoptRockEndpoint()
            .MapGetAdoptableRockByIdEndpoint()
            .MapGetAdoptableRocksEndpoint()
            .MapGetMyPetRocksEndpoint()
            .MapGetAdoptionCertificateEndpoint();

        return app;
    }
}
