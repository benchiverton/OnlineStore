using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Company.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Website.Adoption;

public class AdoptionService
{
    private readonly HttpClient _anonymousHttpClient;
    private readonly HttpClient _authorisedHttpClient;

    public AdoptionService([FromKeyedServices("anonymous")] HttpClient anonymousHttpClient,
        [FromKeyedServices("authorised")] HttpClient authorisedHttpClient)
    {
        _anonymousHttpClient = anonymousHttpClient;
        _authorisedHttpClient = authorisedHttpClient;
    }

    public Task<List<AdoptableRock>> GetAdoptableRocks() =>
        _anonymousHttpClient.GetFromJsonAsync<List<AdoptableRock>>("adoption/rocks");

    public Task<AdoptableRock> GetAdoptableRockById(string petRockId) =>
        _anonymousHttpClient.GetFromJsonAsync<AdoptableRock>($"adoption/rocks/{petRockId}");

    public Task AdoptRock(string petRockId) =>
        _authorisedHttpClient.PostAsync($"adoption/rocks/{petRockId}/adopt", new StringContent(""));
}
