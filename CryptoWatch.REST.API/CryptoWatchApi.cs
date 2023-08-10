using CryptoWatch.REST.API.Paths;

namespace CryptoWatch.REST.API;

public class CryptoWatchRestApi
{
    public const string RootUrl = "https://api.cryptowat.ch";
    private readonly IHttpClientFactory _httpClientFactory;

    public CryptoWatchRestApi(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    public AssetsApi Assets => new(_httpClientFactory);
    public PairsApi Pairs => new(_httpClientFactory);
    public MarketsApi Markets => new(_httpClientFactory);
    public ExchangesApi Exchanges => new(_httpClientFactory);
}
