using CryptoWatch.REST.API;
using CryptoWatch.REST.API.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public sealed class UnauthenticatedAssetsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();

    public UnauthenticatedAssetsTests() =>
        _httpClientFactory.CreateClient(string.Empty)
            .Returns(new HttpClient
            {
                BaseAddress = new Uri(_cryptoWatchServer.Url)
            });

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        _cryptoWatchServer.Dispose();

        return Task.CompletedTask;
    }

    [Fact]
    public async Task Asserts_AssetsDefaultListing_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedAssetsDefaultListingRestEndpoint();

        var assetListing = await new CryptoWatchRestApi(_httpClientFactory).Assets
            .ListAsync();

        assetListing.Should()
            .BeOfType<AssetCollection>();
        assetListing.Result.First()
            .Should()
            .BeOfType<Asset>();
        assetListing.Result.Should()
            .HaveCount(12);
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
    public async Task Asserts_AssetsSpecificAmountListing_JsonResponseDeserialization()
    {
        const uint items = 5;
        _cryptoWatchServer.SetupUnauthenticatedAssetsSpecificAmountListingRestEndpoint(items);

        var assetListing = await new CryptoWatchRestApi(_httpClientFactory).Assets
            .ListAsync(items);

        assetListing.Should()
            .BeOfType<AssetCollection>();
        assetListing.Result.First()
            .Should()
            .BeOfType<Asset>();
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
        assetListing.Allowance.Should()
            .BeOfType<Allowance>();
        assetListing.Allowance.Cost.Should()
            .Be(0.002M);
        assetListing.Allowance.Remaining.Should()
            .Be(9.995M);
        assetListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
        assetListing.Cursor.HasMore.Should()
            .BeTrue();
        assetListing.Cursor.Last.Should()
            .Be("jzNU_tLXCtblCCtyAVH8hik8Q4vGlFrydthCO9b4frYz1VV0WA9HGw");
    }
}
