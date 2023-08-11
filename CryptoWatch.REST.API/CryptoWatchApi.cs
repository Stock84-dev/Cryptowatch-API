using CryptoWatch.REST.API.Paths;

namespace CryptoWatch.REST.API;

public class CryptoWatchRestApi
{
    public const string RootUrl = "https://api.cryptowat.ch";
    private readonly HttpClient _httpClient;

    public CryptoWatchRestApi(HttpClient httpClient) =>
        _httpClient = httpClient;

    public AssetsApi Assets => new(_httpClient);
    public PairsApi Pairs => new(_httpClient);
    public MarketsApi Markets => new(_httpClient);
    public ExchangesApi Exchanges => new(_httpClient);
}
