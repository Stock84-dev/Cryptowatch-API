using System.Net.Http.Json;
using CryptoWatch.REST.API.Types;

namespace CryptoWatch.REST.API.Paths;

public readonly struct PairsApi
{
    private const string Route = "/pairs";
    private readonly HttpClient _httpClient;

    internal PairsApi(HttpClient httpClient) => _httpClient = httpClient;

    public Task<Pairs> ListAsync(CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Pairs>(Route, cancellationToken);

    public Task<Pairs> ListAsync(uint limit, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Pairs>($"{Route}?limit={limit}", cancellationToken);

    public Task<Pairs> ListAsync(string cursor, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Pairs>($"{Route}?cursor={cursor}", cancellationToken);

    public Task<Pairs> ListAsync(uint limit, string cursor, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Pairs>($"{Route}?limit={limit}&cursor={cursor}", cancellationToken);

    public Task<PairDetails> DetailsAsync(string pair, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<PairDetails>($"{Route}/{pair}", cancellationToken);
}
