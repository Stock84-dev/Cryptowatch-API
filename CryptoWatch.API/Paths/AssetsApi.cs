namespace CryptoWatch.API.Paths;

public readonly struct AssetsApi
{
    private const string Route = "/assets";
    private readonly ApiConfiguration _apiConfiguration;

    internal AssetsApi(ApiConfiguration apiConfiguration) => _apiConfiguration = apiConfiguration;

    public Task<HttpResponseMessage> List() =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}");

    public Task<HttpResponseMessage> List(uint limit) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?limit={limit}");

    public Task<HttpResponseMessage> Details(string asset) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{asset}");

    public Task<HttpResponseMessage> Details(string asset, uint limit) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{asset}?limit={limit}");
}