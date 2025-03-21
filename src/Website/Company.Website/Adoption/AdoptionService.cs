using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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

    public Task AdoptRock(AdoptRockRequest request)
    {
        var requestAsJson = JsonSerializer.Serialize(request);
        var content = new StringContent(requestAsJson, Encoding.UTF8, "application/json");
        return _authorisedHttpClient.PostAsync($"adoption/rocks/adopt", content);
    }

    public Task<List<PetRock>> GetPetRocks() =>
        _authorisedHttpClient.GetFromJsonAsync<List<PetRock>>($"adoption/petrocks");

    public Task<Stream> GetAdoptionCertificate(string petRockId) =>
        _authorisedHttpClient.GetStreamAsync($"adoption/petrocks/{petRockId}/certificate");
}
