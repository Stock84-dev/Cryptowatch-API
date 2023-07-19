using CryptoWatch.API.Types;
using FluentAssertions;
using Moq;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public sealed class UnauthenticatedAssetsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();

    public UnauthenticatedAssetsTests()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_cryptoWatchServer.Url)
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
    public async Task Asserts_CryptoWatchApiUnauthenticatedAssetsDefaultListing_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedAssetsDefaultListingRestEndpoint();

        var assetListing = await new CryptoWatchApi(_httpClientFactory.Object).Assets
            .ListAsyncTask();

        assetListing.Should()
            .BeOfType<AssetCollection>();
        assetListing.Result.First()
            .Should()
            .BeOfType<AssetCollection.Collection>();
        assetListing.Result.Should()
            .HaveCount(4842);
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
            .Be("zVeAqhIX15waAlZ__raauv4gLhC-vC_RvnmahCyud7wV0r8yxV5ra9BJgOGmHg");
        assetListing.Allowance.Should()
            .BeOfType<Allowance>();
        assetListing.Allowance.Cost.Should()
            .Be(0.002M);
        assetListing.Allowance.Remaining.Should()
            .Be(9.98M);
        assetListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_CryptoWatchApiUnauthenticatedAssetsSpecificAmountListing_JsonResponseDeserialization()
    {
        const uint items = 5;
        _cryptoWatchServer.SetupUnauthenticatedAssetsSpecificAmountListingRestEndpoint(items);

        var assetListing = await new CryptoWatchApi(_httpClientFactory.Object).Assets
            .ListAsyncTask(items);

        assetListing.Should()
            .BeOfType<AssetCollection>();
        assetListing.Result.First()
            .Should()
            .BeOfType<AssetCollection.Collection>();
        assetListing.Result.Should()
            .HaveCount(5);
        assetListing.Result.First()
            .Fiat.Should()
            .BeFalse();
        assetListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/assets/grc");
        assetListing.Result.First()
            .Id.Should()
            .Be(3);
    }
}