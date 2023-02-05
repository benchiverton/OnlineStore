using Company.Contract;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Company.Website.ProductInfo;

public class ProductInformationService
{
    private readonly HttpClient _httpClient;

    public ProductInformationService(HttpClient httpClient) => _httpClient = httpClient;

    public Task<ProductInformation> GetProductInformationById(string productId) => _httpClient.GetFromJsonAsync<ProductInformation>($"productinformation/{productId}");
}
