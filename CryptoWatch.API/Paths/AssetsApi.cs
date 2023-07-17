using System.Net.Http.Json;
using CryptoWatch.API.Types;

namespace CryptoWatch.API.Paths;

public readonly struct AssetsApi
{
    private const string Route = "/assets";
    private readonly ApiConfiguration _apiConfiguration;

    internal AssetsApi(ApiConfiguration apiConfiguration) => _apiConfiguration = apiConfiguration;

    public Task<AssetCollection> ListAsyncTask() =>
        _apiConfiguration.CreateClient()
            .GetFromJsonAsync<AssetCollection>($"{Route}");

    public Task<HttpResponseMessage> ListAsyncTask(uint limit) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?limit={limit}");

    public Task<HttpResponseMessage> Details(string asset) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{asset}");

    public Task<HttpResponseMessage> Details(string asset, uint limit) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{asset}?limit={limit}");
}