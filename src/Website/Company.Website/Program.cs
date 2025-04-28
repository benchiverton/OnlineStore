using System;
using System.Net.Http;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Company.Website;
using Company.Website.Adoption;
using Company.Website.Authentication.Testing;
using Company.Website.Home;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");

Log.Logger = new LoggerConfiguration()
    .WriteTo.BrowserConsole()
    .CreateLogger();

if (builder.Configuration.GetValue<bool>("UseFakeAuth"))
{
    builder.Services.AddOptions();
    builder.Services.AddAuthorizationCore();
    builder.Services.TryAddScoped<AuthenticationStateProvider, FakeAuthStateProvider>();

    builder.Services.TryAddTransient<BaseAddressAuthorizationMessageHandler>();
    builder.Services.TryAddTransient<AuthorizationMessageHandler>();

    builder.Services.TryAddScoped(sp => (IAccessTokenProvider)sp.GetRequiredService<AuthenticationStateProvider>());

    builder.Services.TryAddScoped<IAccessTokenProviderAccessor, FakeAccessTokenProviderAccessor>();
    builder.Services.TryAddScoped<SignOutSessionStateManager>();
}
else
{
    builder.Services.AddMsalAuthentication(options =>
    {
        builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
        options.ProviderOptions.DefaultAccessTokenScopes.Add(
            $"https://rockpal.onmicrosoft.com/{builder.Configuration["Api:ClientId"]}/access_as_user");
        options.ProviderOptions.LoginMode = "redirect";
    });
}

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

var chatServerConfiguration = builder.Configuration.GetSection("ChatServer").Get<ChatModal.ChatServerConfiguration>();
builder.Services.AddSingleton(chatServerConfiguration);

builder.Services.AddBlazoredSessionStorage();

var host = builder.Build();

await host.RunAsync();
