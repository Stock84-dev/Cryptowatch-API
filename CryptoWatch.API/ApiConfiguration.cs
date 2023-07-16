namespace CryptoWatch.API;

public class ApiConfiguration
{
    private readonly IHttpClientFactory _httpClientFactory;
    private string _apiKey = string.Empty;

    public ApiConfiguration(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public ApiConfiguration(string apiKey, IHttpClientFactory httpClientFactory)
    {
        _apiKey = apiKey;
        _httpClientFactory = httpClientFactory;
    }

    internal HttpClient CreateClient() => _httpClientFactory.CreateClient();
}