using System.Net.Http.Json;
using CryptoWatch.REST.API.Types;

namespace CryptoWatch.REST.API.Paths;

public readonly struct AssetsApi
{
    private const string Route = "/assets";
    private readonly HttpClient _httpClient;

    internal AssetsApi(HttpClient httpClient) => _httpClient = httpClient;

    public Task<AssetCollection> ListAsync(CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<AssetCollection>($"{Route}", cancellationToken);

    public Task<AssetCollection> ListAsync(uint limit, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<AssetCollection>($"{Route}?limit={limit}", cancellationToken);

    public Task<AssetDetail> DetailsAsync(string asset, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<AssetDetail>($"{Route}/{asset}", cancellationToken);

    public Task<AssetDetail> DetailsAsync(string asset, uint limit, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<AssetDetail>($"{Route}/{asset}?limit={limit}", cancellationToken);
}
