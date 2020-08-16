using System;
using System.Net.Http;
using System.Threading.Tasks;
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

namespace Company.Website
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Blazorise, bootstrap, font awesome
            builder.Services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = true;
              })
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

            host.Services
              .UseBootstrapProviders()
              .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
