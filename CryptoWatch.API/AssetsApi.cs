namespace CryptoWatch.API;

public readonly struct AssetsApi
{
    private const string Route = "/assets";
    private readonly ApiConfiguration _apiConfiguration;

    internal AssetsApi(ApiConfiguration apiConfiguration) =>
        _apiConfiguration = apiConfiguration;

    public Task<HttpResponseMessage> List(uint pagination = 5_000) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?limit={pagination}");

    public Task<HttpResponseMessage> Details(string asset, uint pagination = 20_000) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{asset}?limit={pagination}");
}