using System;
using System.Net.Http;
using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Company.Website.Cart;
using Company.Website.Pricing;
using Company.Website.Product;
using Company.Website.ProductInformation;
using Company.Website.ProductVariants;
using Serilog;
using Company.Website;

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
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<ProductInformationService>();
builder.Services.AddTransient<PricingService>();
builder.Services.AddTransient<CurrencyService>();
builder.Services.AddTransient<ProductVariantsService>();
builder.Services.AddScoped<CartService>();

// session storage for the shopping cart
builder.Services.AddBlazoredSessionStorage();

var host = builder.Build();

await host.RunAsync();
