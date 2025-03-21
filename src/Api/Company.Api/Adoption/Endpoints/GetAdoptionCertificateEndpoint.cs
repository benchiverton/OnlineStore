using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Company.Api.Adoption.Dtos;
using Company.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;

namespace Company.Api.Adoption.Endpoints;

public static class GetAdoptionCertificateEndpoint
{
    private static readonly ActivitySource MyActivitySource = new(nameof(GetAdoptionCertificateEndpoint), "1.0.0");

    internal static RouteGroupBuilder MapGetAdoptionCertificateEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("petrocks/{petRockId:guid}/certificate", GetAdoptionCertificate);

        return group;
    }

    private static async Task<IResult> GetAdoptionCertificate(Guid petRockId, AdoptionContext context,
        IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
    {
        using var activity = MyActivitySource.StartActivity();
        var petRock = await context.PetRocks.FindAsync(petRockId);
        var certificate = await GenerateAdoptionCertificate(petRock.FromPetRockDto(), serviceProvider, loggerFactory);
        return Results.File(
            certificate,
            "application/pdf",
            $"certificate");
    }

    private static async Task<byte[]> GenerateAdoptionCertificate(PetRock petRock, IServiceProvider serviceProvider,
        ILoggerFactory loggerFactory)
    {
        await using var renderer = new HtmlRenderer(serviceProvider, loggerFactory);
        var html = await renderer.Dispatcher.InvokeAsync(async () =>
        {
            var dictionary = new Dictionary<string, object> { { "PetRock", petRock } };

            var parameters = ParameterView.FromDictionary(dictionary);
            var output = await renderer.RenderComponentAsync<GetAdoptionCertificateView>(parameters);
            return output.ToHtmlString();
        });

        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
        var page = await browser.NewPageAsync();
        await page.SetContentAsync(html);
        return await page.PdfAsync(new PagePdfOptions { Format = "A4" });
    }
}
