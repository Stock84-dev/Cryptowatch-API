namespace CryptoWatch.API.Paths;

public readonly struct PairsApi
{
    private const string Route = "/pairs";
    private readonly ApiConfiguration _apiConfiguration;

    internal PairsApi(ApiConfiguration apiConfiguration) => _apiConfiguration = apiConfiguration;

    public Task<HttpResponseMessage> List(uint pagination) => _apiConfiguration.CreateClient()
        .GetAsync($"{Route}?limit={pagination}");

    public Task<HttpResponseMessage> Details(string pair) => _apiConfiguration.CreateClient()
        .GetAsync($"{Route}/{pair}");
}