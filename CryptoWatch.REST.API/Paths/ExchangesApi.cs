using System.Net.Http.Json;
using CryptoWatch.REST.API.Types;

namespace CryptoWatch.REST.API.Paths;

public readonly struct ExchangesApi
{
    private const string Route = "/exchanges";
    private readonly IHttpClientFactory _httpClientFactory;

    internal ExchangesApi(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public Task<Exchanges> ListAsync() =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<Exchanges>($"{Route}");

    public Task<Exchange> DetailsAsync(string exchange) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<Exchange>($"{Route}/{exchange}");
}
