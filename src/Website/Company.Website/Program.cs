using System;
using System.Net.Http;
using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Company.Website.ShoppingBasket;
using Company.Website.Pricing;
using Company.Website.Products;
using Serilog;
using Company.Website;
using Microsoft.Extensions.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");

Log.Logger = new LoggerConfiguration()
    .WriteTo.BrowserConsole()
    .CreateLogger();

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Blazorise, bootstrap, font awesome
builder.Services
  .AddBlazorise()
  .AddBootstrapProviders()
  .AddFontAwesomeIcons();

// services
var apiBaseAddress = builder.Configuration.GetValue<string>("Api:BasePath");
builder.Services.AddHttpClient<ProductService>(client => client.BaseAddress = new Uri(apiBaseAddress));
builder.Services.AddHttpClient<PricingService>(client => client.BaseAddress = new Uri(apiBaseAddress));
builder.Services.AddTransient<CurrencyService>();
builder.Services.AddScoped<ShoppingBasketService>();

// session storage for the shopping basket
builder.Services.AddBlazoredSessionStorage();

var host = builder.Build();

await host.RunAsync();
