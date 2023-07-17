namespace CryptoWatch.API.Paths;

public struct ExchangesApi
{
    private const string Route = "/exchanges";
    private readonly ApiConfiguration _apiConfiguration;

    internal ExchangesApi(ApiConfiguration apiConfiguration) => _apiConfiguration = apiConfiguration;

    public Task<HttpResponseMessage> List() =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}");

    public Task<HttpResponseMessage> Details(string exchange) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}");
}