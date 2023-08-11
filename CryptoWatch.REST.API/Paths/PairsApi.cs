using System.Net.Http.Json;
using CryptoWatch.REST.API.Types;

namespace CryptoWatch.REST.API.Paths;

public readonly struct PairsApi
{
    private const string Route = "/pairs";
    private readonly HttpClient _httpClient;

    internal PairsApi(HttpClient httpClient) => _httpClient = httpClient;

    public Task<Pairs> ListAsync() => _httpClient.GetFromJsonAsync<Pairs>(Route);

    public Task<Pairs> ListAsync(uint limit) => _httpClient.GetFromJsonAsync<Pairs>($"{Route}?limit={limit}");

    public Task<Pairs> ListAsync(string cursor) => _httpClient.GetFromJsonAsync<Pairs>($"{Route}?cursor={cursor}");

    public Task<Pairs> ListAsync(uint limit, string cursor) =>
        _httpClient.GetFromJsonAsync<Pairs>($"{Route}?limit={limit}&cursor={cursor}");

    public Task<PairDetails> DetailsAsync(string pair) => _httpClient.GetFromJsonAsync<PairDetails>($"{Route}/{pair}");
}
