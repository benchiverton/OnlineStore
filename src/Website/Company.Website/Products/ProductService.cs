using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Company.Contract;

namespace Company.Website.Products;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient) => _httpClient = httpClient;

    public Task<List<Product>> GetProducts() => _httpClient.GetFromJsonAsync<List<Product>>("products");

    public Task<Product> GetProductById(string productId) => _httpClient.GetFromJsonAsync<Product>($"products/{productId}");
}
