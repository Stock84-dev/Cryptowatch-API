using System.Net.Http.Json;
using CryptoWatch.REST.API.Types;

namespace CryptoWatch.REST.API.Paths;

public readonly struct ExchangesApi
{
    private const string Route = "/exchanges";
    private readonly HttpClient _httpClient;

    internal ExchangesApi(HttpClient httpClient) => _httpClient = httpClient;

    public Task<Exchanges> ListAsync(CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Exchanges>($"{Route}", cancellationToken);

    public Task<Exchange> DetailsAsync(string exchange, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Exchange>($"{Route}/{exchange}", cancellationToken);
}
