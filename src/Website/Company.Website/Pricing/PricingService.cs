using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Company.Contract;

namespace Company.Website.Pricing;

// TODO: rework how pricing is modelled
public class PricingService
{
    private readonly HttpClient _httpClient;

    public PricingService(HttpClient httpClient) => _httpClient = httpClient;

    public Task<Price> GetPriceByProductTypeId(string productVariantId) => _httpClient.GetFromJsonAsync<Price>($"pricing/{productVariantId}");
}
