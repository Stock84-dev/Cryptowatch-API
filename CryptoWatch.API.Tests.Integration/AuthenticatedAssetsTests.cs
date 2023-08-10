using CryptoWatch.REST.API;
using CryptoWatch.REST.API.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public sealed class AuthenticatedAssetsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();

    public AuthenticatedAssetsTests() =>
        _httpClientFactory.CreateClient(string.Empty)
            .Returns(new HttpClient
            {
                BaseAddress = new Uri(_cryptoWatchServer.Url),
                DefaultRequestHeaders = { { "X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF" } }
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
        _cryptoWatchServer.SetupHeaderAuthenticatedAssetsDefaultListingRestEndpoint();

        var assetListing = await new CryptoWatchApi(_httpClientFactory).Assets
            .ListAsync();

        assetListing.Should()
            .BeOfType<AssetCollection>();
        assetListing.Result.First()
            .Should()
            .BeOfType<Asset>();
        assetListing.Result.Should()
            .HaveCount(4);
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

    [Fact]
    public async Task Asserts_AssetsSpecificAmountListing_JsonResponseDeserialization()
    {
        const uint items = 5;
        _cryptoWatchServer.SetupHeaderAuthenticatedAssetsSpecificAmountListingRestEndpoint();

        var assetListing = await new CryptoWatchApi(_httpClientFactory).Assets
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
        assetListing.Result.First()
            .Name.Should()
            .Be("GridCoin");
        assetListing.Result.First()
            .Symbol.Should()
            .Be("grc");
        assetListing.Result.First()
            .SymbolId.Should()
            .Be("gridcoin-research");
        assetListing.Allowance.Should()
            .BeOfType<Allowance>();
        assetListing.Allowance.Cost.Should()
            .Be(0.000M);
        assetListing.Allowance.Remaining.Should()
            .Be(10.000M);
        assetListing.Allowance.Upgrade.Should()
            .BeNull();
        assetListing.Cursor.HasMore.Should()
            .BeTrue();
        assetListing.Cursor.Last.Should()
            .Be("IUNcUONnQ8nOiReTpD3fC9XOTtWZ6_FHJoQz89zcIeULmOsAcmW6Lg");
    }

    [Fact]
    public async Task Asserts_AssetDetailListing_JsonResponseDeserialization()
    {
        const string asset = "btc";
        _cryptoWatchServer.SetupHeaderAuthenticatedAssetDetailRestEndpoint();

        var bitcoinAssetDetails = await new CryptoWatchApi(_httpClientFactory).Assets.DetailsAsync(asset);

        bitcoinAssetDetails.Should()
            .BeOfType<AssetDetail>();
        bitcoinAssetDetails.Result.Should()
            .BeOfType<AssetDetail.ResultDetails>();
        bitcoinAssetDetails.Result.Fiat.Should()
            .BeFalse();
        bitcoinAssetDetails.Result.Id.Should()
            .Be(60);
        bitcoinAssetDetails.Result.Name.Should()
            .Be("Bitcoin");
        bitcoinAssetDetails.Result.Symbol.Should()
            .Be("btc");
        bitcoinAssetDetails.Result.SymbolId.Should()
            .Be("bitcoin");
        bitcoinAssetDetails.Result.AssetMarkets.Should()
            .BeOfType<AssetDetail.Markets>();
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.Should()
            .BeOfType<AssetDetail.Base[]>();
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.Should()
            .BeOfType<AssetDetail.Base[]>();
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.Should()
            .HaveCount(10);
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.First()
            .Id.Should()
            .Be(1);
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.First()
            .Active.Should()
            .BeTrue();
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.First()
            .Exchange.Should()
            .Be("bitfinex");
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.First()
            .Pair.Should()
            .Be("btcusd");
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/bitfinex/btcusd");
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.Should()
            .HaveCount(7);
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.First()
            .Id.Should()
            .Be(3);
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.First()
            .Active.Should()
            .BeTrue();
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.First()
            .Exchange.Should()
            .Be("bitfinex");
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.First()
            .Pair.Should()
            .Be("ltcbtc");
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/bitfinex/ltcbtc");
        bitcoinAssetDetails.Allowance.Should()
            .BeOfType<Allowance>();
        bitcoinAssetDetails.Allowance.Cost.Should()
            .Be(0.0M);
        bitcoinAssetDetails.Allowance.Remaining.Should()
            .Be(10.0M);
        bitcoinAssetDetails.Allowance.RemainingPaid.Should()
            .Be(9999999999);
        bitcoinAssetDetails.Allowance.Upgrade.Should()
            .BeNull();
    }

    [Fact]
    public async Task Asserts_AssetSpecificAmountDetailListing_JsonResponseDeserialization()
    {
        const int items = 5;
        const string asset = "btc";
        _cryptoWatchServer.SetupHeaderAuthenticatedAssetSpecificAmountDetailRestEndpoint();

        var bitcoinAssetDetails = await new CryptoWatchApi(_httpClientFactory).Assets.DetailsAsync(asset, items);

        bitcoinAssetDetails.Should()
            .BeOfType<AssetDetail>();
        bitcoinAssetDetails.Result.Should()
            .BeOfType<AssetDetail.ResultDetails>();
        bitcoinAssetDetails.Result.Fiat.Should()
            .BeFalse();
        bitcoinAssetDetails.Result.Id.Should()
            .Be(60);
        bitcoinAssetDetails.Result.Name.Should()
            .Be("Bitcoin");
        bitcoinAssetDetails.Result.Symbol.Should()
            .Be("btc");
        bitcoinAssetDetails.Result.SymbolId.Should()
            .Be("bitcoin");
        bitcoinAssetDetails.Result.AssetMarkets.Should()
            .BeOfType<AssetDetail.Markets>();
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.Should()
            .BeOfType<AssetDetail.Base[]>();
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.Should()
            .BeOfType<AssetDetail.Base[]>();
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.Should()
            .HaveCount(9);
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.First()
            .Id.Should()
            .Be(1);
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.First()
            .Active.Should()
            .BeTrue();
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.First()
            .Exchange.Should()
            .Be("bitfinex");
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.First()
            .Pair.Should()
            .Be("btcusd");
        bitcoinAssetDetails.Result.AssetMarkets.BaseMarket.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/bitfinex/btcusd");
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.Should()
            .HaveCount(13);
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.First()
            .Id.Should()
            .Be(3);
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.First()
            .Active.Should()
            .BeTrue();
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.First()
            .Exchange.Should()
            .Be("bitfinex");
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.First()
            .Pair.Should()
            .Be("ltcbtc");
        bitcoinAssetDetails.Result.AssetMarkets.QuoteMarket.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/bitfinex/ltcbtc");
        bitcoinAssetDetails.Allowance.Should()
            .BeOfType<Allowance>();
        bitcoinAssetDetails.Allowance.Cost.Should()
            .Be(0.0M);
        bitcoinAssetDetails.Allowance.Remaining.Should()
            .Be(10.0M);
        bitcoinAssetDetails.Allowance.RemainingPaid.Should()
            .Be(9999999999);
        bitcoinAssetDetails.Allowance.Upgrade.Should()
            .BeNull();
    }

    [Fact]
    public async Task Asserts_InvalidlyAuthenticated_Response()
    {
        _cryptoWatchServer.SetupHeaderInvalidlyAuthenticatedAssetsDefaultListingRestEndpoint();
        _httpClientFactory.CreateClient(string.Empty)
            .Returns(new HttpClient
            {
                BaseAddress = new Uri(_cryptoWatchServer.Url),
                DefaultRequestHeaders = { { "X-CW-API-Key", "---" } }
            });

        var invalidCall = async () => await new CryptoWatchApi(_httpClientFactory).Assets.ListAsync();

        await invalidCall.Should()
            .ThrowAsync<HttpRequestException>()
            .WithMessage("Response status code does not indicate success: 400 (Bad Request).");
    }
}
