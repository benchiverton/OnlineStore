using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Company.Contract;

namespace Company.Website.RockSelection;

public class PetRockService
{
    private readonly HttpClient _httpClient;

    public PetRockService(HttpClient httpClient) => _httpClient = httpClient;

    public Task<List<PetRock>> GetPetRocks() => _httpClient.GetFromJsonAsync<List<PetRock>>("petrocks");

    public Task<PetRock> GetPetRockById(string petRockId) => _httpClient.GetFromJsonAsync<PetRock>($"petrocks/{petRockId}");

    public Task<List<Variant>> GetPetRockVariants(string petRockId) => _httpClient.GetFromJsonAsync<List<Variant>>($"petrocks/{petRockId}/variants");

    public Task<Variant> GetPetRockVariantById(string petRockId, string variantId) => _httpClient.GetFromJsonAsync<Variant>($"petrocks/{petRockId}/variants/{variantId}");
}
