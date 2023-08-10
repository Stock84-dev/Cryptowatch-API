using CryptoWatch.API.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public sealed class UnauthenticatedPairsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();

    public UnauthenticatedPairsTests() =>
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
    public async Task Asserts_PairsDefaultListing_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedPairsDefaultListingRestEndpoint();

        var pairsListing = await new CryptoWatchApi(_httpClientFactory).Pairs
            .ListAsync();

        pairsListing.Should()
            .BeOfType<Pairs>();
        pairsListing.Result.Should()
            .BeOfType<Pairs.ResultDetails[]>();
        pairsListing.Result.Should()
            .HaveCount(14);
        pairsListing.Result.First()
            .Id.Should()
            .Be(185_927);
        pairsListing.Result.First()
            .Base.Should()
            .BeOfType<Asset>();
        pairsListing.Result.First()
            .Base.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .Base.Id.Should()
            .Be(7_900);
        pairsListing.Result.First()
            .Base.Name.Should()
            .Be("Stone");
        pairsListing.Result.First()
            .Base.Route.Should()
            .Be("https://api.cryptowat.ch/assets/0ne");
        pairsListing.Result.First()
            .Base.Symbol.Should()
            .Be("0ne");
        pairsListing.Result.First()
            .FuturesContractPeriod.Should()
            .BeNull();
        pairsListing.Result.First()
            .Quote.Should()
            .BeOfType<Asset>();
        pairsListing.Result.First()
            .Quote.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .Quote.Id.Should()
            .Be(5_080);
        pairsListing.Result.First()
            .Quote.Name.Should()
            .Be("Wrapped Ether");
        pairsListing.Result.First()
            .Quote.Route.Should()
            .Be("https://api.cryptowat.ch/assets/weth");
        pairsListing.Result.First()
            .Quote.Symbol.Should()
            .Be("weth");
        pairsListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/pairs/0neweth");
        pairsListing.Result.First()
            .Symbol.Should()
            .Be("0neweth");
        pairsListing.Cursor.Should()
            .BeOfType<Cursor>();
        pairsListing.Cursor.HasMore.Should()
            .BeTrue();
        pairsListing.Cursor.Last.Should()
            .Be("5Vdb6T1Q1hguLvic_rMsR8q_4MymIyNB2PlhjEVrMlRJ6xXaCrqvowS98nXC");
        pairsListing.Allowance.Should()
            .BeOfType<Allowance>();
        pairsListing.Allowance.Cost.Should()
            .Be(0.003M);
        pairsListing.Allowance.Remaining.Should()
            .Be(9.989M);
        pairsListing.Allowance.RemainingPaid.Should()
            .Be(0);
        pairsListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_PairsSpecificAmountListing_JsonResponseDeserialization()
    {
        const int items = 5;
        _cryptoWatchServer.SetupUnauthenticatedPairsSpecificAmountListingRestEndpoint();

        var pairsListing = await new CryptoWatchApi(_httpClientFactory).Pairs.ListAsync(items);

        pairsListing.Should()
            .BeOfType<Pairs>();
        pairsListing.Result.Should()
            .BeOfType<Pairs.ResultDetails[]>();
        pairsListing.Result.Should()
            .HaveCount(5);
        pairsListing.Result.First()
            .Base.Should()
            .BeOfType<Asset>();
        pairsListing.Result.First()
            .Base.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .Base.Id.Should()
            .Be(88);
        pairsListing.Result.First()
            .Base.Name.Should()
            .Be("EOS");
        pairsListing.Result.First()
            .Base.Route.Should()
            .Be("https://api.cryptowat.ch/assets/eos");
        pairsListing.Result.First()
            .Base.Symbol.Should()
            .Be("eos");
        pairsListing.Result.First()
            .FuturesContractPeriod.Should()
            .BeNull();
        pairsListing.Result.First()
            .Id.Should()
            .Be(4);
        pairsListing.Result.First()
            .Quote.Should()
            .BeOfType<Asset>();
        pairsListing.Result.First()
            .Quote.Fiat.Should()
            .BeTrue();
        pairsListing.Result.First()
            .Quote.Id.Should()
            .Be(98);
        pairsListing.Result.First()
            .Quote.Name.Should()
            .Be("United States Dollar");
        pairsListing.Result.First()
            .Quote.Route.Should()
            .Be("https://api.cryptowat.ch/assets/usd");
        pairsListing.Result.First()
            .Quote.Symbol.Should()
            .Be("usd");
        pairsListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/pairs/eosusd");
        pairsListing.Result.First()
            .Symbol.Should()
            .Be("eosusd");
        pairsListing.Cursor.Should()
            .BeOfType<Cursor>();
        pairsListing.Cursor.Last.Should()
            .Be("S_v4gQoCByt1snk8oSuh670Q_QU1ZRSDlA9igxjER8lWsXXj6geogA");
        pairsListing.Cursor.HasMore.Should()
            .BeTrue();
        pairsListing.Allowance.Should()
            .BeOfType<Allowance>();
        pairsListing.Allowance.Cost.Should()
            .Be(0.003M);
        pairsListing.Allowance.Remaining.Should()
            .Be(9.997M);
        pairsListing.Allowance.RemainingPaid.Should()
            .Be(0);
        pairsListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task
        Asserts_PairsListingWithCursor_JsonResponseDeserialization()
    {
        const string cursor = "S_v4gQoCByt1snk8oSuh670Q_QU1ZRSDlA9igxjER8lWsXXj6geogA";

        _cryptoWatchServer.SetupUnauthenticatedPairsListingWithCursorRestEndpoint();

        var pairsListing = await new CryptoWatchApi(_httpClientFactory).Pairs.ListAsync(cursor);

        pairsListing.Should()
            .BeOfType<Pairs>();
        pairsListing.Result.Should()
            .BeOfType<Pairs.ResultDetails[]>();
        pairsListing.Result.Should()
            .HaveCount(21);
        pairsListing.Result.First()
            .Base.Should()
            .BeOfType<Asset>();
        pairsListing.Result.First()
            .Base.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .Base.Id.Should()
            .Be(7900);
        pairsListing.Result.First()
            .Base.Name.Should()
            .Be("Stone");
        pairsListing.Result.First()
            .Base.Route.Should()
            .Be("https://api.cryptowat.ch/assets/0ne");
        pairsListing.Result.First()
            .Base.Symbol.Should()
            .Be("0ne");
        pairsListing.Result.First()
            .FuturesContractPeriod.Should()
            .BeNull();
        pairsListing.Result.First()
            .Id.Should()
            .Be(185_927);
        pairsListing.Result.First()
            .Quote.Should()
            .BeOfType<Asset>();
        pairsListing.Result.First()
            .Quote.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .Quote.Id.Should()
            .Be(5080);
        pairsListing.Result.First()
            .Quote.Name.Should()
            .Be("Wrapped Ether");
        pairsListing.Result.First()
            .Quote.Route.Should()
            .Be("https://api.cryptowat.ch/assets/weth");
        pairsListing.Result.First()
            .Quote.Symbol.Should()
            .Be("weth");
        pairsListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/pairs/0neweth");
        pairsListing.Result.First()
            .Symbol.Should()
            .Be("0neweth");
        pairsListing.Cursor.Should()
            .BeOfType<Cursor>();
        pairsListing.Cursor.HasMore.Should()
            .BeTrue();
        pairsListing.Cursor.Last.Should()
            .Be("gQkdFcNTdOhXSmWXDIDfywL0PbzdlCmfL_9nf7ra2ZcpLGVmPmmdBdRae_WG");
        pairsListing.Allowance.Should()
            .BeOfType<Allowance>();
        pairsListing.Allowance.Cost.Should()
            .Be(0.003M);
        pairsListing.Allowance.Remaining.Should()
            .Be(9.997M);
        pairsListing.Allowance.RemainingPaid.Should()
            .Be(0);
        pairsListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task
        Asserts_PairsSpecificAmountWithCursorListing_JsonResponseDeserialization()
    {
        const int items = 2;
        const string cursor = "S_v4gQoCByt1snk8oSuh670Q_QU1ZRSDlA9igxjER8lWsXXj6geogA";

        _cryptoWatchServer.SetupUnauthenticatedPairsSpecificAmountWithCursorListingRestEndpoint();

        var pairsListing = await new CryptoWatchApi(_httpClientFactory).Pairs.ListAsync(items, cursor);

        pairsListing.Should()
            .BeOfType<Pairs>();
        pairsListing.Result.Should()
            .BeOfType<Pairs.ResultDetails[]>();
        pairsListing.Result.Should()
            .HaveCount(2);
        pairsListing.Result.First()
            .Base.Should()
            .BeOfType<Asset>();
        pairsListing.Result.First()
            .Base.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .Base.Id.Should()
            .Be(72);
        pairsListing.Result.First()
            .Base.Name.Should()
            .Be("Dash");
        pairsListing.Result.First()
            .Base.Route.Should()
            .Be("https://api.cryptowat.ch/assets/dash");
        pairsListing.Result.First()
            .Base.Symbol.Should()
            .Be("dash");
        pairsListing.Result.First()
            .FuturesContractPeriod.Should()
            .BeNull();
        pairsListing.Result.First()
            .Id.Should()
            .Be(6);
        pairsListing.Result.First()
            .Quote.Should()
            .BeOfType<Asset>();
        pairsListing.Result.First()
            .Quote.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .Quote.Id.Should()
            .Be(60);
        pairsListing.Result.First()
            .Quote.Name.Should()
            .Be("Bitcoin");
        pairsListing.Result.First()
            .Quote.Route.Should()
            .Be("https://api.cryptowat.ch/assets/btc");
        pairsListing.Result.First()
            .Quote.Symbol.Should()
            .Be("btc");
        pairsListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/pairs/dashbtc");
        pairsListing.Result.First()
            .Symbol.Should()
            .Be("dashbtc");
        pairsListing.Cursor.Should()
            .BeOfType<Cursor>();
        pairsListing.Cursor.HasMore.Should()
            .BeTrue();
        pairsListing.Cursor.Last.Should()
            .Be("AwnhW7dcktI47BJ5lFJA-cxAy8h_eM25nbc53pzwEAwNnOMPTKvjvw");
        pairsListing.Allowance.Should()
            .BeOfType<Allowance>();
        pairsListing.Allowance.Cost.Should()
            .Be(0.003M);
        pairsListing.Allowance.Remaining.Should()
            .Be(9.997M);
        pairsListing.Allowance.RemainingPaid.Should()
            .Be(0);
        pairsListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_PairsDefaultDetail_JsonResponseDeserialization()
    {
        const string pair = "0neweth";
        _cryptoWatchServer.SetupUnauthenticatedPairsDefaultDetailRestEndpoint();

        var pairDetails = await new CryptoWatchApi(_httpClientFactory).Pairs.DetailsAsync(pair);

        pairDetails.Should()
            .BeOfType<PairDetails>();
        pairDetails.Result.Should()
            .BeOfType<PairDetails.ResultDetails>();
        pairDetails.Result.Id.Should()
            .Be(185_927);
        pairDetails.Result.BasePair.Should()
            .BeOfType<Asset>();
        pairDetails.Result.BasePair.Fiat.Should()
            .BeFalse();
        pairDetails.Result.BasePair.Id.Should()
            .Be(7_900);
        pairDetails.Result.BasePair.Name.Should()
            .Be("Stone");
        pairDetails.Result.BasePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/0ne");
        pairDetails.Result.BasePair.Symbol.Should()
            .Be("0ne");
        pairDetails.Result.Markets.Should()
            .BeOfType<MarketDetails[]>();
        pairDetails.Result.Markets.Should()
            .HaveCount(1);
        pairDetails.Result.Markets.First()
            .Active.Should()
            .BeTrue();
        pairDetails.Result.Markets.First()
            .Exchange.Should()
            .Be("uniswap-v2");
        pairDetails.Result.Markets.First()
            .Id.Should()
            .Be(2_917_710);
        pairDetails.Result.Markets.First()
            .Pair.Should()
            .Be("0neweth");
        pairDetails.Result.Markets.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/uniswap-v2/0neweth");
        pairDetails.Result.QuotePair.Should()
            .BeOfType<Asset>();
        pairDetails.Result.QuotePair.Fiat.Should()
            .BeFalse();
        pairDetails.Result.QuotePair.Id.Should()
            .Be(5_080);
        pairDetails.Result.QuotePair.Name.Should()
            .Be("Wrapped Ether");
        pairDetails.Result.QuotePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/weth");
        pairDetails.Result.QuotePair.Symbol.Should()
            .Be("weth");
        pairDetails.Result.Route.Should()
            .Be("https://api.cryptowat.ch/pairs/0neweth");
        pairDetails.Result.Symbol.Should()
            .Be("0neweth");
        pairDetails.Allowance.Should()
            .BeOfType<Allowance>();
        pairDetails.Allowance.Cost.Should()
            .Be(0.002M);
        pairDetails.Allowance.Remaining.Should()
            .Be(9.998M);
        pairDetails.Allowance.RemainingPaid.Should()
            .Be(0);
        pairDetails.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }
}
