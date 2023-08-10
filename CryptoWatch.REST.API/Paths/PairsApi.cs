using System.Net.Http.Json;
using CryptoWatch.REST.API.Types;

namespace CryptoWatch.REST.API.Paths;

public readonly struct PairsApi
{
    private const string Route = "/pairs";
    private readonly IHttpClientFactory _httpClientFactory;

    internal PairsApi(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public Task<Pairs> ListAsync() => _httpClientFactory.CreateClient()
        .GetFromJsonAsync<Pairs>(Route);

    public Task<Pairs> ListAsync(uint limit) => _httpClientFactory.CreateClient()
        .GetFromJsonAsync<Pairs>($"{Route}?limit={limit}");

    public Task<Pairs> ListAsync(string cursor) => _httpClientFactory.CreateClient()
        .GetFromJsonAsync<Pairs>($"{Route}?cursor={cursor}");

    public Task<Pairs> ListAsync(uint limit, string cursor) => _httpClientFactory.CreateClient()
        .GetFromJsonAsync<Pairs>($"{Route}?limit={limit}&cursor={cursor}");

    public Task<PairDetails> DetailsAsync(string pair) => _httpClientFactory.CreateClient()
        .GetFromJsonAsync<PairDetails>($"{Route}/{pair}");
}
