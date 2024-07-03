using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Company.Contract;

namespace Company.Website.Adoption;

public class AdoptionService
{
    private readonly HttpClient _httpClient;

    public AdoptionService(HttpClient httpClient) => _httpClient = httpClient;

    public Task<List<AdoptableRock>> GetAdoptableRocks() =>
        _httpClient.GetFromJsonAsync<List<AdoptableRock>>("adoption/rocks");

    public Task<AdoptableRock> GetAdoptableRockById(string petRockId) =>
        _httpClient.GetFromJsonAsync<AdoptableRock>($"adoption/rocks/{petRockId}");
}
