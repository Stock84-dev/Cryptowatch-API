namespace CryptoWatch.API;

public class CryptoWatchApi
{
    public const string RootUrl = "https://api.cryptowat.ch";
    private readonly ApiConfiguration _apiConfiguration;

    public CryptoWatchApi(IHttpClientFactory httpClientFactory) =>
        _apiConfiguration = new ApiConfiguration(httpClientFactory);

    public CryptoWatchApi(string apiKey, IHttpClientFactory httpClientFactory) =>
        _apiConfiguration = new ApiConfiguration(apiKey, httpClientFactory);

    public CryptoWatchApi(ApiConfiguration apiConfiguration) => _apiConfiguration = apiConfiguration;

    public AssetsApi Assets => new(_apiConfiguration);
    public PairsApi Pairs => new(_apiConfiguration);
    public MarketsApi Markets => new(_apiConfiguration);
    public ExchangesApi Exchanges => new(_apiConfiguration);
}