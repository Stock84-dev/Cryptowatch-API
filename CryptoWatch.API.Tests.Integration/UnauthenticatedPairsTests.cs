using CryptoWatch.API.Types;
using FluentAssertions;
using Moq;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public sealed class UnauthenticatedPairsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();

    public UnauthenticatedPairsTests()
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
    public async Task Asserts_PairsDefaultListing_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedPairsDefaultListingRestEndpoint();

        var pairsListing = await new CryptoWatchApi(_httpClientFactory.Object).Pairs
            .List();

        pairsListing.Should()
            .BeOfType<Pairs>();
        pairsListing.Result.Should()
            .BeOfType<List<Pairs.ResultCollection>>();
        pairsListing.Result.Should()
            .HaveCount(15_000);
        pairsListing.Result.First()
            .Id.Should()
            .Be(185_927);
        pairsListing.Result.First()
            .BasePair.Should()
            .BeOfType<Base>();
        pairsListing.Result.First()
            .BasePair.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .BasePair.Id.Should()
            .Be(7_900);
        pairsListing.Result.First()
            .BasePair.Name.Should()
            .Be("Stone");
        pairsListing.Result.First()
            .BasePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/0ne");
        pairsListing.Result.First()
            .BasePair.Symbol.Should()
            .Be("0ne");
        pairsListing.Result.First()
            .FuturesContractPeriod.Should()
            .BeNull();
        pairsListing.Result.First()
            .QuotePair.Should()
            .BeOfType<Quote>();
        pairsListing.Result.First()
            .QuotePair.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .QuotePair.Id.Should()
            .Be(5_080);
        pairsListing.Result.First()
            .QuotePair.Name.Should()
            .Be("Wrapped Ether");
        pairsListing.Result.First()
            .QuotePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/weth");
        pairsListing.Result.First()
            .QuotePair.Symbol.Should()
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

        var pairsListing = await new CryptoWatchApi(_httpClientFactory.Object).Pairs.List(items);

        pairsListing.Should()
            .BeOfType<Pairs>();
        pairsListing.Result.Should()
            .BeOfType<List<Pairs.ResultCollection>>();
        pairsListing.Result.Should()
            .HaveCount(5);
        pairsListing.Result.First()
            .BasePair.Should()
            .BeOfType<Base>();
        pairsListing.Result.First()
            .BasePair.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .BasePair.Id.Should()
            .Be(88);
        pairsListing.Result.First()
            .BasePair.Name.Should()
            .Be("EOS");
        pairsListing.Result.First()
            .BasePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/eos");
        pairsListing.Result.First()
            .BasePair.Symbol.Should()
            .Be("eos");
        pairsListing.Result.First()
            .FuturesContractPeriod.Should()
            .BeNull();
        pairsListing.Result.First()
            .Id.Should()
            .Be(4);
        pairsListing.Result.First()
            .QuotePair.Should()
            .BeOfType<Quote>();
        pairsListing.Result.First()
            .QuotePair.Fiat.Should()
            .BeTrue();
        pairsListing.Result.First()
            .QuotePair.Id.Should()
            .Be(98);
        pairsListing.Result.First()
            .QuotePair.Name.Should()
            .Be("United States Dollar");
        pairsListing.Result.First()
            .QuotePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/usd");
        pairsListing.Result.First()
            .QuotePair.Symbol.Should()
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

        var pairsListing = await new CryptoWatchApi(_httpClientFactory.Object).Pairs.List(cursor);

        pairsListing.Should()
            .BeOfType<Pairs>();
        pairsListing.Result.Should()
            .BeOfType<List<Pairs.ResultCollection>>();
        pairsListing.Result.Should()
            .HaveCount(15_000);
        pairsListing.Result.First()
            .BasePair.Should()
            .BeOfType<Base>();
        pairsListing.Result.First()
            .BasePair.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .BasePair.Id.Should()
            .Be(7900);
        pairsListing.Result.First()
            .BasePair.Name.Should()
            .Be("Stone");
        pairsListing.Result.First()
            .BasePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/0ne");
        pairsListing.Result.First()
            .BasePair.Symbol.Should()
            .Be("0ne");
        pairsListing.Result.First()
            .FuturesContractPeriod.Should()
            .BeNull();
        pairsListing.Result.First()
            .Id.Should()
            .Be(185_927);
        pairsListing.Result.First()
            .QuotePair.Should()
            .BeOfType<Quote>();
        pairsListing.Result.First()
            .QuotePair.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .QuotePair.Id.Should()
            .Be(5080);
        pairsListing.Result.First()
            .QuotePair.Name.Should()
            .Be("Wrapped Ether");
        pairsListing.Result.First()
            .QuotePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/weth");
        pairsListing.Result.First()
            .QuotePair.Symbol.Should()
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

        var pairsListing = await new CryptoWatchApi(_httpClientFactory.Object).Pairs.List(items, cursor);

        pairsListing.Should()
            .BeOfType<Pairs>();
        pairsListing.Result.Should()
            .BeOfType<List<Pairs.ResultCollection>>();
        pairsListing.Result.Should()
            .HaveCount(2);
        pairsListing.Result.First()
            .BasePair.Should()
            .BeOfType<Base>();
        pairsListing.Result.First()
            .BasePair.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .BasePair.Id.Should()
            .Be(72);
        pairsListing.Result.First()
            .BasePair.Name.Should()
            .Be("Dash");
        pairsListing.Result.First()
            .BasePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/dash");
        pairsListing.Result.First()
            .BasePair.Symbol.Should()
            .Be("dash");
        pairsListing.Result.First()
            .FuturesContractPeriod.Should()
            .BeNull();
        pairsListing.Result.First()
            .Id.Should()
            .Be(6);
        pairsListing.Result.First()
            .QuotePair.Should()
            .BeOfType<Quote>();
        pairsListing.Result.First()
            .QuotePair.Fiat.Should()
            .BeFalse();
        pairsListing.Result.First()
            .QuotePair.Id.Should()
            .Be(60);
        pairsListing.Result.First()
            .QuotePair.Name.Should()
            .Be("Bitcoin");
        pairsListing.Result.First()
            .QuotePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/btc");
        pairsListing.Result.First()
            .QuotePair.Symbol.Should()
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

        var pairDetails = await new CryptoWatchApi(_httpClientFactory.Object).Pairs.Details(pair);

        pairDetails.Should()
            .BeOfType<PairDetails>();
        pairDetails.Result.Should()
            .BeOfType<PairDetails.ResultCollection>();
        pairDetails.Result.id.Should()
            .Be(185_927);
        pairDetails.Result.basePair.Should()
            .BeOfType<Base>();
        pairDetails.Result.basePair.Fiat.Should()
            .BeFalse();
        pairDetails.Result.basePair.Id.Should()
            .Be(7_900);
        pairDetails.Result.basePair.Name.Should()
            .Be("Stone");
        pairDetails.Result.basePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/0ne");
        pairDetails.Result.basePair.Symbol.Should()
            .Be("0ne");
        pairDetails.Result.markets.Should()
            .BeOfType<List<MarketDetails>>();
        pairDetails.Result.markets.Should()
            .HaveCount(1);
        pairDetails.Result.markets.First()
            .Active.Should()
            .BeTrue();
        pairDetails.Result.markets.First()
            .Exchange.Should()
            .Be("uniswap-v2");
        pairDetails.Result.markets.First()
            .Id.Should()
            .Be(2_917_710);
        pairDetails.Result.markets.First()
            .Pair.Should()
            .Be("0neweth");
        pairDetails.Result.markets.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/uniswap-v2/0neweth");
        pairDetails.Result.quotePair.Should()
            .BeOfType<Quote>();
        pairDetails.Result.quotePair.Fiat.Should()
            .BeFalse();
        pairDetails.Result.quotePair.Id.Should()
            .Be(5_080);
        pairDetails.Result.quotePair.Name.Should()
            .Be("Wrapped Ether");
        pairDetails.Result.quotePair.Route.Should()
            .Be("https://api.cryptowat.ch/assets/weth");
        pairDetails.Result.quotePair.Symbol.Should()
            .Be("weth");
        pairDetails.Result.route.Should()
            .Be("https://api.cryptowat.ch/pairs/0neweth");
        pairDetails.Result.symbol.Should()
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