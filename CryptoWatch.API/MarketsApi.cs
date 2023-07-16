namespace CryptoWatch.API;

public readonly struct MarketsApi
{
    private const uint Pagination = 20_000;
    private const string Route = "/markets";
    private readonly ApiConfiguration _apiConfiguration;

    internal MarketsApi(ApiConfiguration apiConfiguration) =>
        _apiConfiguration = apiConfiguration;

    public Task<HttpResponseMessage> List() =>
        _apiConfiguration.CreateClient()
            .GetAsync(Route);

    public Task<HttpResponseMessage> List(string cursor) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?cursor={cursor}");

    public Task<HttpResponseMessage> List(uint pagination) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?limit={pagination}");

    public Task<HttpResponseMessage> List(string cursor, uint pagination) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?cursor={cursor}&limit={pagination}");

    public Task<HttpResponseMessage> Details(string exchange, string pair) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}");

    public Task<HttpResponseMessage> Price() =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/prices");

    public Task<HttpResponseMessage> Price(string cursor) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/prices?cursor={cursor}");

    public Task<HttpResponseMessage> Price(string cursor, uint limit) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?cursor={cursor}&limit={limit}");

    public Task<HttpResponseMessage> Price(string exchange, string pair) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/price");

    public Task<HttpResponseMessage> Trades(string exchange, string pair) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/trades");
    
    public Task<HttpResponseMessage> Trades(string exchange, string pair, int since) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/trades?since={since}");

    public Task<HttpResponseMessage> Trades(string exchange, string pair, int since, int pagination) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/trades?since={since}&limit={pagination}");
}