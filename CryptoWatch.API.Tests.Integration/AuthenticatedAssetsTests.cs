using CryptoWatch.API.Types;
using FluentAssertions;
using Moq;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public sealed class AuthenticatedAssetsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();

    public AuthenticatedAssetsTests()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_cryptoWatchServer.Url),
            DefaultRequestHeaders = { { "X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF" } }
        };

        _httpClientFactory.Setup(x => x.CreateClient(string.Empty))
            .Returns(httpClient);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        _cryptoWatchServer.Dispose();

        return Task.CompletedTask;
    }

    [Fact]
    public async Task Asserts_CryptoWatchApiAuthenticatedAssetsDefaultListing_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupHeaderAuthenticatedAssetsDefaultListingRestEndpoint();

        var assetListing = await new CryptoWatchApi(_httpClientFactory.Object).Assets
            .ListAsyncTask();

        assetListing.Should()
            .BeOfType<AssetCollection>();
        assetListing.Result.First()
            .Should()
            .BeOfType<AssetCollection.Collection>();
        assetListing.Result.Should()
            .HaveCount(4845);
        assetListing.Result.First()
            .Fiat.Should()
            .BeFalse();
        assetListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/assets/00");
        assetListing.Result.First()
            .Id.Should()
            .Be(182298);
        assetListing.Result.First()
            .Name.Should()
            .Be("zer0zer0");
        assetListing.Result.First()
            .Symbol.Should()
            .Be("00");
        assetListing.Cursor.Should()
            .BeOfType<Cursor>();
        assetListing.Cursor.HasMore.Should()
            .BeFalse();
        assetListing.Cursor.Last.Should()
            .Be("oyf5zGBCmihzyxUAuRQBgqnpQMbdUiwrR6Av0zu51i_10bJhWKgiXiqq7EkBTg");
        assetListing.Allowance.Should()
            .BeOfType<Allowance>();
        assetListing.Allowance.Cost.Should()
            .Be(0.000M);
        assetListing.Allowance.Remaining.Should()
            .Be(10.000M);
        assetListing.Allowance.Upgrade.Should()
            .BeNull();
    }
}