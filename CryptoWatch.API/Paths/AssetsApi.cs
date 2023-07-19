using System.Net.Http.Json;
using CryptoWatch.API.Types;

namespace CryptoWatch.API.Paths;

public readonly struct AssetsApi
{
    private const string Route = "/assets";
    private readonly IHttpClientFactory _httpClientFactory;

    internal AssetsApi(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public Task<AssetCollection> ListAsyncTask() =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<AssetCollection>($"{Route}");

    public Task<AssetCollection> ListAsyncTask(uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<AssetCollection>($"{Route}?limit={limit}");

    public Task<HttpResponseMessage> Details(string asset) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{asset}");

    public Task<HttpResponseMessage> Details(string asset, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{asset}?limit={limit}");
}