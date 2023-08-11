using System.Net.Http.Json;
using CryptoWatch.REST.API.Types;

namespace CryptoWatch.REST.API.Paths;

public readonly struct ExchangesApi
{
    private const string Route = "/exchanges";
    private readonly HttpClient _httpClient;

    internal ExchangesApi(HttpClient httpClient) => _httpClient = httpClient;

    public Task<Exchanges> ListAsync() =>
        _httpClient.GetFromJsonAsync<Exchanges>($"{Route}");

    public Task<Exchange> DetailsAsync(string exchange) =>
        _httpClient.GetFromJsonAsync<Exchange>($"{Route}/{exchange}");
}
