using System;
using System.Net.Http;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Company.Website;
using Company.Website.Adoption;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");

Log.Logger = new LoggerConfiguration()
    .WriteTo.BrowserConsole()
    .CreateLogger();

// services
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add(
        $"https://rockpal.onmicrosoft.com/{builder.Configuration["Api:ClientId"]}/access_as_user");
    options.ProviderOptions.LoginMode = "redirect";
});
builder.Services.AddCascadingAuthenticationState();

var apiBaseAddress = builder.Configuration.GetValue<string>("Api:BasePath");
builder.Services.AddKeyedScoped("anonymous", (_, _) => new HttpClient { BaseAddress = new Uri(apiBaseAddress) });
builder.Services.AddScoped<AuthorizationMessageHandler>();
builder.Services
    .AddHttpClient("AuthorisedHttp", client => client.BaseAddress = new Uri(apiBaseAddress))
    .AddHttpMessageHandler(sp =>
        sp.GetRequiredService<AuthorizationMessageHandler>()
            .ConfigureHandler(authorizedUrls: [apiBaseAddress]));
builder.Services.AddKeyedScoped("authorised",
    (ctx, _) => ctx.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorisedHttp"));

builder.Services.AddTransient<AdoptionService>();

builder.Services.AddBlazoredSessionStorage();

var host = builder.Build();

await host.RunAsync();
