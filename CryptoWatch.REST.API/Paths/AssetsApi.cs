using System.Net.Http.Json;
using CryptoWatch.REST.API.Types;

namespace CryptoWatch.REST.API.Paths;

public readonly struct AssetsApi
{
    private const string Route = "/assets";
    private readonly HttpClient _httpClient;

    internal AssetsApi(HttpClient httpClient) => _httpClient = httpClient;

    public Task<AssetCollection> ListAsync() =>
        _httpClient.GetFromJsonAsync<AssetCollection>($"{Route}");

    public Task<AssetCollection> ListAsync(uint limit) =>
        _httpClient.GetFromJsonAsync<AssetCollection>($"{Route}?limit={limit}");

    public Task<AssetDetail> DetailsAsync(string asset) =>
        _httpClient.GetFromJsonAsync<AssetDetail>($"{Route}/{asset}");

    public Task<AssetDetail> DetailsAsync(string asset, uint limit) =>
        _httpClient.GetFromJsonAsync<AssetDetail>($"{Route}/{asset}?limit={limit}");
}
