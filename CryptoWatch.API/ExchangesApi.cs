namespace CryptoWatch.API;

public struct ExchangesApi
{
    private readonly ApiConfiguration _apiConfiguration;
    internal ExchangesApi(ApiConfiguration apiConfiguration) =>
        _apiConfiguration = apiConfiguration;
}