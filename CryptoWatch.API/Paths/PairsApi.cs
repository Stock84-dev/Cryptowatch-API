using System.Net.Http.Json;
using CryptoWatch.API.Types;

namespace CryptoWatch.API.Paths;

public readonly struct PairsApi
{
    private const string Route = "/pairs";
    private readonly IHttpClientFactory _httpClientFactory;

    internal PairsApi(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public Task<Pairs> List() => _httpClientFactory.CreateClient()
        .GetFromJsonAsync<Pairs>(Route);

    public Task<HttpResponseMessage> List(uint limit) => _httpClientFactory.CreateClient()
        .GetAsync($"{Route}?limit={limit}");

    public Task<HttpResponseMessage> List(string cursor) => _httpClientFactory.CreateClient()
        .GetAsync($"{Route}?cursor={cursor}");

    public Task<HttpResponseMessage> List(uint limit, string cursor) => _httpClientFactory.CreateClient()
        .GetAsync($"{Route}?limit={limit}&cursor={cursor}");

    public Task<HttpResponseMessage> Details(string pair) => _httpClientFactory.CreateClient()
        .GetAsync($"{Route}/{pair}");
}