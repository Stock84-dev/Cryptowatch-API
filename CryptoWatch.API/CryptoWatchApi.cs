using CryptoWatch.API.Paths;

namespace CryptoWatch.API;

public class CryptoWatchApi
{
    public const string RootUrl = "https://api.cryptowat.ch";
    private readonly IHttpClientFactory _httpClientFactory;

    public CryptoWatchApi(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    public AssetsApi Assets => new(_httpClientFactory);
    public PairsApi Pairs => new(_httpClientFactory);
    public MarketsApi Markets => new(_httpClientFactory);
    public ExchangesApi Exchanges => new(_httpClientFactory);
}
