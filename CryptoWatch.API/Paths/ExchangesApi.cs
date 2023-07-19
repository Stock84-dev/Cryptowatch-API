namespace CryptoWatch.API.Paths;

public readonly struct ExchangesApi
{
    private const string Route = "/exchanges";
    private readonly IHttpClientFactory _httpClientFactory;

    internal ExchangesApi(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public Task<HttpResponseMessage> List() =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}");

    public Task<HttpResponseMessage> Details(string exchange) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}");
}