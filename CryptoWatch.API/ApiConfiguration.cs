namespace CryptoWatch.API;

public class ApiConfiguration
{
    private string _apiKey = string.Empty;
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiConfiguration(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    public ApiConfiguration(string apiKey, IHttpClientFactory httpClientFactory)
    {
        _apiKey = apiKey;
        _httpClientFactory = httpClientFactory;
    }

    internal HttpClient CreateClient() =>
        _httpClientFactory.CreateClient();
}