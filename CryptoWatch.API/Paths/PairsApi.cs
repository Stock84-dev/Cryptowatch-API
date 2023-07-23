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

    public Task<Pairs> List(uint limit) => _httpClientFactory.CreateClient()
        .GetFromJsonAsync<Pairs>($"{Route}?limit={limit}");

    public Task<Pairs> List(string cursor) => _httpClientFactory.CreateClient()
        .GetFromJsonAsync<Pairs>($"{Route}?cursor={cursor}");

    public Task<Pairs> List(uint limit, string cursor) => _httpClientFactory.CreateClient()
        .GetFromJsonAsync<Pairs>($"{Route}?limit={limit}&cursor={cursor}");

    public Task<PairDetails> Details(string pair) => _httpClientFactory.CreateClient()
        .GetFromJsonAsync<PairDetails>($"{Route}/{pair}");
}