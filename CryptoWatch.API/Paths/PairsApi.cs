namespace CryptoWatch.API.Paths;

public readonly struct PairsApi
{
    private const string Route = "/pairs";
    private readonly IHttpClientFactory _httpClientFactory;

    internal PairsApi(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public Task<HttpResponseMessage> List(uint pagination) => _httpClientFactory.CreateClient()
        .GetAsync($"{Route}?limit={pagination}");

    public Task<HttpResponseMessage> Details(string pair) => _httpClientFactory.CreateClient()
        .GetAsync($"{Route}/{pair}");
}