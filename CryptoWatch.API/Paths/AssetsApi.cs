using System.Net.Http.Json;
using CryptoWatch.API.Types;

namespace CryptoWatch.API.Paths;

public readonly struct AssetsApi
{
    private const string Route = "/assets";
    private readonly IHttpClientFactory _httpClientFactory;

    internal AssetsApi(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public Task<AssetCollection> ListAsync() =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<AssetCollection>($"{Route}");

    public Task<AssetCollection> ListAsync(uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<AssetCollection>($"{Route}?limit={limit}");

    public Task<AssetDetail> DetailsAsync(string asset) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<AssetDetail>($"{Route}/{asset}");

    public Task<AssetDetail> DetailsAsync(string asset, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<AssetDetail>($"{Route}/{asset}?limit={limit}");
}