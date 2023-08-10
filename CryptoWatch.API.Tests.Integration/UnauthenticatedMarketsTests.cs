using System.ComponentModel;
using CryptoWatch.API.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public class UnauthenticatedMarketsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();

    public UnauthenticatedMarketsTests() =>
        _httpClientFactory.CreateClient(string.Empty).Returns(new HttpClient
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
    public async Task Asserts_MarketsListing_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedDefaultListingMarketsRestEndpoint();

        var marketListing = await new CryptoWatchApi(_httpClientFactory).Markets.ListAsync();

        marketListing.Should()
            .BeOfType<MarketCollection>();
        marketListing.Result.Should()
            .BeOfType<MarketDetails[]>();
        marketListing.Result.First()
            .Should()
            .BeOfType<MarketDetails>();
        marketListing.Result.Should()
            .HaveCount(2);
        marketListing.Result.First()
            .Active.Should()
            .BeTrue();
        marketListing.Result.First()
            .Exchange.Should()
            .Be("bitfinex");
        marketListing.Result.First()
            .Id.Should()
            .Be(1);
        marketListing.Result.First()
            .Pair.Should()
            .Be("btcusd");
        marketListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/bitfinex/btcusd");
        marketListing.Cursor.Should()
            .BeOfType<Cursor>();
        marketListing.Cursor.HasMore.Should()
            .BeTrue();
        marketListing.Cursor.Last.Should()
            .Be("TF8j1fnzBNxi7bQkOQgcFb2r9X_jzp0jq8PmiYcAnGzjlUHY93Sg7AdMzlzpvQ");
        marketListing.Allowance.Should()
            .BeOfType<Allowance>();
        marketListing.Allowance.Cost.Should()
            .Be(0.003M);
        marketListing.Allowance.Remaining.Should()
            .Be(9.997M);
        marketListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_MarketsListingFromCursor_JsonResponseDeserialization()
    {
        const string cursor = "TF8j1fnzBNxi7bQkOQgcFb2r9X_jzp0jq8PmiYcAnGzjlUHY93Sg7AdMzlzpvQ";
        _cryptoWatchServer.SetupUnauthenticatedMarketsListingFromCursorRestEndpoint();

        var marketListing = await new CryptoWatchApi(_httpClientFactory).Markets.ListAsync(cursor);

        marketListing.Should()
            .BeOfType<MarketCollection>();
        marketListing.Result.Should()
            .BeOfType<MarketDetails[]>();
        marketListing.Result.First()
            .Should()
            .BeOfType<MarketDetails>();
        marketListing.Result.Should()
            .HaveCount(2);
        marketListing.Result.First()
            .Active.Should()
            .BeTrue();
        marketListing.Result.First()
            .Exchange.Should()
            .Be("kraken");
        marketListing.Result.First()
            .Id.Should()
            .Be(3_025_661);
        marketListing.Result.First()
            .Pair.Should()
            .Be("egldusd");
        marketListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/kraken/egldusd");
        marketListing.Cursor.Should()
            .BeOfType<Cursor>();
        marketListing.Cursor.HasMore.Should()
            .BeFalse();
        marketListing.Cursor.Last.Should()
            .Be("BWgumN0awa71xjUYPqlzbIDvzN7sCIa85I9A4z-TUK-2amfy9co0x5cCw3ojbQ");
        marketListing.Allowance.Should()
            .BeOfType<Allowance>();
        marketListing.Allowance.Cost.Should()
            .Be(0.003M);
        marketListing.Allowance.Remaining.Should()
            .Be(9.997M);
        marketListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_SpecificMarketAmountListing_JsonResponseDeserialization()
    {
        const int items = 3;
        _cryptoWatchServer.SetupUnauthenticatedThreeMarketsListingRestEndpoint();

        var marketListing = await new CryptoWatchApi(_httpClientFactory).Markets.ListAsync(items);

        marketListing.Should()
            .BeOfType<MarketCollection>();
        marketListing.Result.Should()
            .BeOfType<MarketDetails[]>();
        marketListing.Result.Should()
            .HaveCount(3);
        marketListing.Result.First()
            .Active.Should()
            .BeTrue();
        marketListing.Result.First()
            .Exchange.Should()
            .Be("bitfinex");
        marketListing.Result.First()
            .Id.Should()
            .Be(1);
        marketListing.Result.First()
            .Pair.Should()
            .Be("btcusd");
        marketListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/bitfinex/btcusd");
        marketListing.Cursor.Should()
            .BeOfType<Cursor>();
        marketListing.Cursor.HasMore.Should()
            .BeTrue();
        marketListing.Cursor.Last.Should()
            .Be("SdgMYB9J1JiK7ejL21NoCqHcRT1eb6tTAIXZ12jGbKzEiPa-xpLZOg");
        marketListing.Allowance.Should()
            .BeOfType<Allowance>();
        marketListing.Allowance.Cost.Should()
            .Be(0.003M);
        marketListing.Allowance.Remaining.Should()
            .Be(9.997M);
        marketListing.Allowance.RemainingPaid.Should()
            .Be(0);
        marketListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_SpecificMarketAmountFromCursorListing_JsonResponseDeserialization()
    {
        const int item = 3;
        const string cursor = "SdgMYB9J1JiK7ejL21NoCqHcRT1eb6tTAIXZ12jGbKzEiPa-xpLZOg";
        _cryptoWatchServer.SetupUnauthenticatedThreeMarketsWithCursorListingRestEndpoint();

        var marketListing = await new CryptoWatchApi(_httpClientFactory).Markets.ListAsync(cursor, item);

        marketListing.Should()
            .BeOfType<MarketCollection>();
        marketListing.Result.Should()
            .BeOfType<MarketDetails[]>();
        marketListing.Result.Should()
            .HaveCount(3);
        marketListing.Result.First()
            .Active.Should()
            .BeTrue();
        marketListing.Result.First()
            .Exchange.Should()
            .Be("bitfinex");
        marketListing.Result.First()
            .Id.Should()
            .Be(4);
        marketListing.Result.First()
            .Pair.Should()
            .Be("ethusd");
        marketListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/bitfinex/ethusd");
        marketListing.Cursor.Should()
            .BeOfType<Cursor>();
        marketListing.Cursor.HasMore.Should()
            .BeTrue();
        marketListing.Cursor.Last.Should()
            .Be("OJTrGLSIYkgY2kt4kwdcVEAQhxEHyuruKNJXzIYsH0XSyPGA46tcsg");
        marketListing.Allowance.Should()
            .BeOfType<Allowance>();
        marketListing.Allowance.Cost.Should()
            .Be(0.003M);
        marketListing.Allowance.Remaining.Should()
            .Be(9.997M);
        marketListing.Allowance.RemainingPaid.Should()
            .Be(0);
        marketListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_PairMarketDetail_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        _cryptoWatchServer.SetupUnauthenticatedPairMarketDetailRestEndpoint();

        var marketPairDetail = await new CryptoWatchApi(_httpClientFactory).Markets.DetailsAsync(exchange, pair);

        marketPairDetail.Should()
            .BeOfType<MarketPairDetail>();
        marketPairDetail.Result.Active.Should()
            .BeTrue();
        marketPairDetail.Result.Exchange.Should()
            .Be("kraken");
        marketPairDetail.Result.Id.Should()
            .Be(87);
        marketPairDetail.Result.Pair.Should()
            .Be("btcusd");
        marketPairDetail.Result.Routes.Should()
            .BeOfType<Routes>();
        marketPairDetail.Result.Routes.Price.Should()
            .Be("https://api.cryptowat.ch/markets/kraken/btcusd/price");
        marketPairDetail.Result.Routes.Summary.Should()
            .Be("https://api.cryptowat.ch/markets/kraken/btcusd/summary");
        marketPairDetail.Result.Routes.Orderbook.Should()
            .Be("https://api.cryptowat.ch/markets/kraken/btcusd/orderbook");
        marketPairDetail.Result.Routes.Trades.Should()
            .Be("https://api.cryptowat.ch/markets/kraken/btcusd/trades");
        marketPairDetail.Result.Routes.Ohlc.Should()
            .Be("https://api.cryptowat.ch/markets/kraken/btcusd/ohlc");
        marketPairDetail.Allowance.Should()
            .BeOfType<Allowance>();
        marketPairDetail.Allowance.Cost.Should()
            .Be(0.002M);
        marketPairDetail.Allowance.Remaining.Should()
            .Be(9.998M);
        marketPairDetail.Allowance.RemainingPaid.Should()
            .Be(0);
        marketPairDetail.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_MarketsPrices_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedMarketsPricesRestEndpoint();

        var marketsPrices = await new CryptoWatchApi(_httpClientFactory).Markets.PriceAsync();

        marketsPrices.Should()
            .BeOfType<MarketPrices>();
        marketsPrices.Result.Should()
            .BeOfType<Dictionary<string, decimal>>();
        marketsPrices.Result.Should()
            .HaveCount(8);
        marketsPrices.Result.Should()
            .ContainKey("market:binance-us:adausdt");
        marketsPrices.Result.First()
            .Key.Should()
            .Be("market:binance-us:1inchusdt");
        marketsPrices.Result.First()
            .Should()
            .BeAssignableTo<KeyValuePair<string, decimal>>()
            .Which.Value
            .Should()
            .Be(0.306M);
        marketsPrices.Cursor.HasMore.Should()
            .BeFalse();
        marketsPrices.Cursor.Last.Should()
            .Be("glg3WNiFROtazDy4FwXzD7neFdB-fbbD8Sy3VDiAbvMPZqOmoftpk_ufP2-COA");
        marketsPrices.Allowance.Cost.Should()
            .Be(0.015M);
        marketsPrices.Allowance.Remaining.Should()
            .Be(9.985M);
        marketsPrices.Allowance.RemainingPaid.Should()
            .Be(0);
        marketsPrices.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_MarketsPricesWithCursor_JsonResponseDeserialization()
    {
        const string cursor = "BDj0fwwHBUM7Rz4YNJvyhM1vMO5PyygjB-AAht0UbizZZ7_VqEB1JA";
        _cryptoWatchServer.SetupUnauthenticatedMarketsPricesWithCursorRestEndpoint();

        var marketsPrices = await new CryptoWatchApi(_httpClientFactory).Markets.PriceAsync(cursor);

        marketsPrices.Should()
            .BeOfType<MarketPrices>();
        marketsPrices.Result.Should()
            .BeOfType<Dictionary<string, decimal>>();
        marketsPrices.Result.Should()
            .HaveCount(24);
        marketsPrices.Result.Should()
            .ContainKey("market:binance-us:avaxusdt");
        marketsPrices.Result.First()
            .Key.Should()
            .Be("market:binance-us:1inchusdt");
        marketsPrices.Result.First()
            .Should()
            .BeAssignableTo<KeyValuePair<string, decimal>>()
            .Which.Value
            .Should()
            .Be(0.304M);
        marketsPrices.Cursor.HasMore.Should()
            .BeFalse();
        marketsPrices.Cursor.Last.Should()
            .Be("KhI-xNYlTLEiTY50mWXVCp_KDi6dfNlLjM-dHxb5EpxtnB4XjlRZ_UtUwBlzAQ");
        marketsPrices.Allowance.Cost.Should()
            .Be(0.015M);
        marketsPrices.Allowance.Remaining.Should()
            .Be(9.985M);
        marketsPrices.Allowance.RemainingPaid.Should()
            .Be(0);
        marketsPrices.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_MarketsPricesWithCursorAndLimitOfThree_JsonResponseDeserialization()
    {
        const int items = 3;
        const string cursor = "BDj0fwwHBUM7Rz4YNJvyhM1vMO5PyygjB-AAht0UbizZZ7_VqEB1JA";
        _cryptoWatchServer.SetupUnauthenticatedMarketsPricesWithCursorAndLimitOfThreeRestEndpoint();

        var marketsPrices = await new CryptoWatchApi(_httpClientFactory).Markets.PriceAsync(cursor, items);

        marketsPrices.Should()
            .BeOfType<MarketPrices>();
        marketsPrices.Result.Should()
            .BeOfType<Dictionary<string, decimal>>();
        marketsPrices.Result.Should()
            .HaveCount(20);
        marketsPrices.Result.Should()
            .ContainKey("market:binance-us:avaxbtc");
        marketsPrices.Result.First()
            .Key.Should()
            .Be("market:binance-us:1inchusdt");
        marketsPrices.Result.First()
            .Should()
            .BeAssignableTo<KeyValuePair<string, decimal>>()
            .Which.Value
            .Should()
            .Be(0.304M);
        marketsPrices.Cursor.Should()
            .BeOfType<Cursor>();
        marketsPrices.Cursor.HasMore.Should()
            .BeTrue();
        marketsPrices.Cursor.Last.Should()
            .Be("NiBZVMA38VIwgRHf7b_jbQNNjTM9ZwJizlQWvU4vXc6QkcdyzxMLIw");
        marketsPrices.Allowance.Should()
            .BeOfType<Allowance>();
        marketsPrices.Allowance.Cost.Should()
            .Be(0.015M);
        marketsPrices.Allowance.Remaining.Should()
            .Be(9.985M);
        marketsPrices.Allowance.RemainingPaid.Should()
            .Be(0);
        marketsPrices.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_PairMarketPrice_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        _cryptoWatchServer.SetupUnauthenticatedPairMarketDetailPriceRestEndpoint();

        var marketPairPrice = await new CryptoWatchApi(_httpClientFactory).Markets.PriceAsync(exchange, pair);

        marketPairPrice.Should()
            .BeOfType<MarketPairPrice>();
        marketPairPrice.Result.Should()
            .BeOfType<MarketPairPrice.PriceResult>();
        marketPairPrice.Result.Price.Should()
            .Be(29_080.70M);
        marketPairPrice.Allowance.Should()
            .BeOfType<Allowance>();
        marketPairPrice.Allowance.Cost.Should()
            .Be(0.005M);
        marketPairPrice.Allowance.Remaining.Should()
            .Be(9.995M);
        marketPairPrice.Allowance.RemainingPaid.Should()
            .Be(0);
        marketPairPrice.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_MostRecentTrades_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        _cryptoWatchServer.SetupUnauthenticatedMarketMostRecentTradesOfAPairRestEndpoint();

        var mostRecentTrades = await new CryptoWatchApi(_httpClientFactory).Markets.TradesAsync(exchange, pair);

        mostRecentTrades.Should()
            .BeOfType<MostRecentTrades>();
        mostRecentTrades.Result.Should()
            .BeOfType<decimal[][]>();
        mostRecentTrades.RecentTrades.Should()
            .BeOfType<RecentTrade[]>();
        mostRecentTrades.Count.Should()
            .Be(50);
        mostRecentTrades.RecentTrades.Should()
            .HaveCount(50);
        mostRecentTrades.Result.Should()
            .HaveCount(50);
        mostRecentTrades[0]
            .Should()
            .BeEquivalentTo(mostRecentTrades.RecentTrades.First());
        mostRecentTrades[0]
            .Amount.Should()
            .Be(0.00094621M);
        mostRecentTrades[0]
            .Id.Should()
            .Be(0);
        mostRecentTrades[0]
            .Price.Should()
            .Be(29135.1M);
        mostRecentTrades[0]
            .Timestamp.Should()
            .Be(1690337054);
        mostRecentTrades.Allowance.Should()
            .BeOfType<Allowance>()
            .Which.Remaining.Should()
            .Be(9.99M);
        mostRecentTrades.Allowance.Cost.Should()
            .Be(0.01M);
        mostRecentTrades.Allowance.RemainingPaid.Should()
            .Be(0);
        mostRecentTrades.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_MostRecentTradesWithTimestamp_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const int cursor = 1690337054;
        _cryptoWatchServer.SetupUnauthenticatedMarketMostRecentTradesOfAPairWithTimestampRestEndpoint();

        var mostRecentTrades =
            await new CryptoWatchApi(_httpClientFactory).Markets.TradesAsync(exchange, pair, cursor);

        mostRecentTrades.Should()
            .BeOfType<MostRecentTrades>();
        mostRecentTrades.Result.Should()
            .BeOfType<decimal[][]>();
        mostRecentTrades.RecentTrades.Should()
            .BeOfType<RecentTrade[]>();
        mostRecentTrades.Count.Should()
            .Be(50);
        mostRecentTrades.RecentTrades.Should()
            .HaveCount(50);
        mostRecentTrades.Result.Should()
            .HaveCount(50);
        mostRecentTrades[0]
            .Should()
            .BeEquivalentTo(mostRecentTrades.RecentTrades.First());
        mostRecentTrades[0]
            .Amount.Should()
            .Be(0.75689032M);
        mostRecentTrades[0]
            .Id.Should()
            .Be(0);
        mostRecentTrades[0]
            .Price.Should()
            .Be(0.06353M);
        mostRecentTrades[0]
            .Timestamp.Should()
            .Be(1690408640);
        mostRecentTrades.Allowance.Should()
            .BeOfType<Allowance>()
            .Which.Remaining.Should()
            .Be(9.99M);
        mostRecentTrades.Allowance.Cost.Should()
            .Be(0.01M);
        mostRecentTrades.Allowance.RemainingPaid.Should()
            .Be(0);
        mostRecentTrades.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_MostRecentTradesWithTimestampAndLimit_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const int cursor = 1690337054;
        const int limit = 5;
        _cryptoWatchServer.SetupUnauthenticatedMarketMostRecentTradesOfAPairWithTimestampAndLimitRestEndpoint();

        var mostRecentTrades =
            await new CryptoWatchApi(_httpClientFactory).Markets.TradesAsync(exchange, pair, cursor, limit);

        mostRecentTrades.Should()
            .BeOfType<MostRecentTrades>();
        mostRecentTrades.Result.Should()
            .BeOfType<decimal[][]>();
        mostRecentTrades.RecentTrades.Should()
            .BeOfType<RecentTrade[]>();
        mostRecentTrades.Count.Should()
            .Be(5);
        mostRecentTrades.RecentTrades.Should()
            .HaveCount(5);
        mostRecentTrades.Result.Should()
            .HaveCount(5);
        mostRecentTrades[0]
            .Should()
            .BeEquivalentTo(mostRecentTrades.RecentTrades.First());
        mostRecentTrades[0]
            .Amount.Should()
            .Be(0.01246267M);
        mostRecentTrades[0]
            .Id.Should()
            .Be(0);
        mostRecentTrades[0]
            .Price.Should()
            .Be(1871.56M);
        mostRecentTrades[0]
            .Timestamp.Should()
            .Be(1690417655);
        mostRecentTrades.Allowance.Should()
            .BeOfType<Allowance>()
            .Which.Remaining.Should()
            .Be(9.99M);
        mostRecentTrades.Allowance.Cost.Should()
            .Be(0.01M);
        mostRecentTrades.Allowance.RemainingPaid.Should()
            .Be(0);
        mostRecentTrades.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_MarketPairSummary_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        _cryptoWatchServer.SetupUnauthenticatedMarketPairSummaryRestEndpoint();

        var marketPairSummary =
            await new CryptoWatchApi(_httpClientFactory).Markets.SummaryAsync(exchange, pair);

        marketPairSummary.Should()
            .BeOfType<Summary>();
        marketPairSummary.Result.Should()
            .BeOfType<Summary.ResultDetail>();
        marketPairSummary.Result.Price.Change.Absolute.Should()
            .Be(177);
        marketPairSummary.Result.Price.Change.Percentage.Should()
            .Be(0.0060699588477366251);
        marketPairSummary.Result.Price.High.Should()
            .Be(29678.299999999999);
        marketPairSummary.Result.Price.Last.Should()
            .Be(29337);
        marketPairSummary.Result.Price.Low.Should()
            .Be(29105.5);
        marketPairSummary.Result.Volume.Should()
            .Be(2047.7691799822398);
        marketPairSummary.Result.VolumeQuote.Should()
            .Be(60061803.852393046);
        marketPairSummary.Allowance.Should()
            .BeOfType<Allowance>();
        marketPairSummary.Allowance.Cost.Should()
            .Be(0.005M);
        marketPairSummary.Allowance.Remaining.Should()
            .Be(9.995M);
        marketPairSummary.Allowance.RemainingPaid.Should()
            .Be(0);
        marketPairSummary.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_AllMarketsSummaries_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedAllMarketsSummariesRestEndpoint();

        var allMarketsSummaries = await new CryptoWatchApi(_httpClientFactory).Markets.SummariesAsync();

        allMarketsSummaries.Should()
            .BeOfType<Summaries>();
        allMarketsSummaries.Result.Should()
            .BeOfType<Dictionary<string, Summaries.ResultDetail>>()
            .Which.Should()
            .HaveCount(2);
        allMarketsSummaries.Result.Should()
            .ContainKey("binance-us:aaveusdt");
        allMarketsSummaries.Result.First()
            .Key.Should()
            .Be("binance-us:1inchusdt");
        allMarketsSummaries.Result.First()
            .Value.Price.Change.Absolute.Should()
            .Be(0.0020000000000000018);
        allMarketsSummaries.Result.First()
            .Value.Price.Change.Percentage.Should()
            .Be(0.0066225165562913968);
        allMarketsSummaries.Result.First()
            .Value.Price.High.Should()
            .Be(0.308);
        allMarketsSummaries.Result.First()
            .Value.Price.Last.Should()
            .Be(0.30399999999999999);
        allMarketsSummaries.Result.First()
            .Value.Price.Low.Should()
            .Be(0.29699999999999999);
        allMarketsSummaries.Result.First()
            .Value.Volume.Should()
            .Be(1_440.4999999999627);
        allMarketsSummaries.Result.First()
            .Value.VolumeBase.Should()
            .Be(1_440.4999999999627);
        allMarketsSummaries.Result.First()
            .Value.VolumeQuote.Should()
            .Be(437.85069999994812);
        allMarketsSummaries.Result.First()
            .Value.VolumeUsd.Should()
            .Be(437.86940701357082);
        allMarketsSummaries.Allowance.Cost.Should()
            .Be(0.015M);
        allMarketsSummaries.Allowance.Remaining.Should()
            .Be(9.985M);
        allMarketsSummaries.Allowance.RemainingPaid.Should()
            .Be(0);
        allMarketsSummaries.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
        allMarketsSummaries.Cursor.HasMore.Should()
            .BeFalse();
        allMarketsSummaries.Cursor.Last.Should()
            .Be("cl8nR6LCLsALNQGrz_1HbTbWfyBwkYhLWNUnY-faI5bCLr5QWC193gRm-1SmOg");
    }

    [Fact]
    public async Task Asserts_MarketsSummariesWithLimitOf3_JsonResponseDeserialization()
    {
        const int limit = 3;
        _cryptoWatchServer.SetupUnauthenticatedMarketsSummariesWithLimitOf3RestEndpoint();

        var allMarketsSummaries = await new CryptoWatchApi(_httpClientFactory).Markets.SummariesAsync(limit);

        allMarketsSummaries.Should()
            .BeOfType<Summaries>();
        allMarketsSummaries.Result.Should()
            .BeOfType<Dictionary<string, Summaries.ResultDetail>>()
            .Which.Should()
            .HaveCount(3);
        allMarketsSummaries.Result.Should()
            .ContainKey("bitfinex:ltcbtc");
        allMarketsSummaries.Result.First()
            .Key.Should()
            .Be("bitfinex:btcusd");
        allMarketsSummaries.Result.First()
            .Value.Price.Change.Absolute.Should()
            .Be(-53);
        allMarketsSummaries.Result.First()
            .Value.Price.Change.Percentage.Should()
            .Be(-0.0018020468532181837);
        allMarketsSummaries.Result.First()
            .Value.Price.High.Should()
            .Be(29_444);
        allMarketsSummaries.Result.First()
            .Value.Price.Last.Should()
            .Be(29_358);
        allMarketsSummaries.Result.First()
            .Value.Price.Low.Should()
            .Be(29_305);
        allMarketsSummaries.Result.First()
            .Value.Volume.Should()
            .Be(149.79487410440294);
        allMarketsSummaries.Result.First()
            .Value.VolumeBase.Should()
            .Be(149.79487410440294);
        allMarketsSummaries.Result.First()
            .Value.VolumeQuote.Should()
            .Be(4_397_565.4728781907);
        allMarketsSummaries.Result.First()
            .Value.VolumeUsd.Should()
            .Be(4_397_565.4728781907);
        allMarketsSummaries.Allowance.Cost.Should()
            .Be(0.015M);
        allMarketsSummaries.Allowance.Remaining.Should()
            .Be(9.985M);
        allMarketsSummaries.Allowance.RemainingPaid.Should()
            .Be(0);
        allMarketsSummaries.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
        allMarketsSummaries.Cursor.HasMore.Should()
            .BeTrue();
        allMarketsSummaries.Cursor.Last.Should()
            .Be("jYWBofYi7AbqxVQyHC3GQoguYnxKEL2vjPxTCJ3SAZEcXdzN6HDnSw");
    }

    [Fact]
    public async Task Asserts_MarketsSummariesFromCursorWithLimitOf3_JsonResponseDeserialization()
    {
        const int limit = 3;
        const string cursor = "jYWBofYi7AbqxVQyHC3GQoguYnxKEL2vjPxTCJ3SAZEcXdzN6HDnSw";
        _cryptoWatchServer.SetupUnauthenticatedMarketsSummariesFromCursorWithLimitOf3RestEndpoint();

        var allMarketsSummaries =
            await new CryptoWatchApi(_httpClientFactory).Markets.SummariesAsync(cursor, limit);

        allMarketsSummaries.Should()
            .BeOfType<Summaries>();
        allMarketsSummaries.Result.Should()
            .BeOfType<Dictionary<string, Summaries.ResultDetail>>()
            .Which.Should()
            .HaveCount(3);
        allMarketsSummaries.Result.Should()
            .ContainKey("bitfinex:ethbtc");
        allMarketsSummaries.Result.First()
            .Key.Should()
            .Be("bitfinex:etcbtc");
        allMarketsSummaries.Result.First()
            .Value.Price.Change.Absolute.Should()
            .Be(4.8099999999999489E-06);
        allMarketsSummaries.Result.First()
            .Value.Price.Change.Percentage.Should()
            .Be(0.0076513163127335544);
        allMarketsSummaries.Result.First()
            .Value.Price.High.Should()
            .Be(0.00063540999999999999);
        allMarketsSummaries.Result.First()
            .Value.Price.Last.Should()
            .Be(0.00063345999999999997);
        allMarketsSummaries.Result.First()
            .Value.Price.Low.Should()
            .Be(0.00062785);
        allMarketsSummaries.Result.First()
            .Value.Volume.Should()
            .Be(498.58893223001542);
        allMarketsSummaries.Result.First()
            .Value.VolumeBase.Should()
            .Be(498.58893223001542);
        allMarketsSummaries.Result.First()
            .Value.VolumeQuote.Should()
            .Be(0.31553935055387394);
        allMarketsSummaries.Result.First()
            .Value.VolumeUsd.Should()
            .Be(9_263.4347054919381);
        allMarketsSummaries.Allowance.Cost.Should()
            .Be(0.015M);
        allMarketsSummaries.Allowance.Remaining.Should()
            .Be(9.985M);
        allMarketsSummaries.Allowance.RemainingPaid.Should()
            .Be(0);
        allMarketsSummaries.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
        allMarketsSummaries.Cursor.HasMore.Should()
            .BeTrue();
        allMarketsSummaries.Cursor.Last.Should()
            .Be("U2a6cHlYunhmlcObd2lcQtZUg_jW9jLedi5oAY3v-6Xm7S4Y1rfPdQ");
    }

    [Fact]
    public async Task Asserts_AllMarketsSummariesById_JsonResponseDeserialization()
    {
        const string keyBy = "id";
        _cryptoWatchServer.SetupUnauthenticatedAllMarketsSummariesByIdRestEndpoint();

        var allMarketsSummaries = await new CryptoWatchApi(_httpClientFactory).Markets.SummariesAsync(keyBy);

        allMarketsSummaries.Should()
            .BeOfType<Summaries>();
        allMarketsSummaries.Result.Should()
            .BeOfType<Dictionary<string, Summaries.ResultDetail>>()
            .Which.Should()
            .HaveCount(2);
        allMarketsSummaries.Result.Should()
            .ContainKey("100");
        allMarketsSummaries.Result.First()
            .Key.Should()
            .Be("1");
        allMarketsSummaries.Result.First()
            .Value.Price.Change.Absolute.Should()
            .Be(-21);
        allMarketsSummaries.Result.First()
            .Value.Price.Change.Percentage.Should()
            .Be(-0.0007143100105445764);
        allMarketsSummaries.Result.First()
            .Value.Price.High.Should()
            .Be(29_444);
        allMarketsSummaries.Result.First()
            .Value.Price.Last.Should()
            .Be(29_378);
        allMarketsSummaries.Result.First()
            .Value.Price.Low.Should()
            .Be(29_305);
        allMarketsSummaries.Result.First()
            .Value.Volume.Should()
            .Be(151.92321649440277);
        allMarketsSummaries.Result.First()
            .Value.VolumeBase.Should()
            .Be(151.92321649440277);
        allMarketsSummaries.Result.First()
            .Value.VolumeQuote.Should()
            .Be(4_460_209.3021574402);
        allMarketsSummaries.Result.First()
            .Value.VolumeUsd.Should()
            .Be(4_460_209.3021574402);
        allMarketsSummaries.Allowance.Cost.Should()
            .Be(0.015M);
        allMarketsSummaries.Allowance.Remaining.Should()
            .Be(9.985M);
        allMarketsSummaries.Allowance.RemainingPaid.Should()
            .Be(0);
        allMarketsSummaries.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
        allMarketsSummaries.Cursor.HasMore.Should()
            .BeFalse();
        allMarketsSummaries.Cursor.Last.Should()
            .Be("N8Hl9uDjJ8EzouTNEg8UjpbjB4sKdfmdvAlDewqbD8ZX-qCANbfM8kcTs7_p9w");
    }

    [Fact]
    public async Task Asserts_AllMarketsSummariesByIdFromCursor_JsonResponseDeserialization()
    {
        const string keyBy = "id";
        const string cursor = "gCLPH49ToTXjmDnq2VyXCtu5HtSV__AiJU7XjdF1hAwu48pFj0_G70zdGrrKvg";
        _cryptoWatchServer.SetupUnauthenticatedAllMarketsSummariesByIdFromCursorRestEndpoint();

        var allMarketsSummaries =
            await new CryptoWatchApi(_httpClientFactory).Markets.SummariesAsync(keyBy, cursor);

        allMarketsSummaries.Should()
            .BeOfType<Summaries>();
        allMarketsSummaries.Result.Should()
            .BeOfType<Dictionary<string, Summaries.ResultDetail>>()
            .Which.Should()
            .HaveCount(3);
        allMarketsSummaries.Result.Should()
            .ContainKey("3763781");
        allMarketsSummaries.Result.First()
            .Key.Should()
            .Be("3763626");
        allMarketsSummaries.Result.First()
            .Value.Price.Change.Absolute.Should()
            .Be(0.088600000000000012);
        allMarketsSummaries.Result.First()
            .Value.Price.Change.Percentage.Should()
            .Be(0.032721497950289918);
        allMarketsSummaries.Result.First()
            .Value.Price.High.Should()
            .Be(3.4790000000000001);
        allMarketsSummaries.Result.First()
            .Value.Price.Last.Should()
            .Be(2.7963);
        allMarketsSummaries.Result.First()
            .Value.Price.Low.Should()
            .Be(2.6789000000000001);
        allMarketsSummaries.Result.First()
            .Value.Volume.Should()
            .Be(1_813.4000000000426);
        allMarketsSummaries.Result.First()
            .Value.VolumeBase.Should()
            .Be(1_813.4000000000426);
        allMarketsSummaries.Result.First()
            .Value.VolumeQuote.Should()
            .Be(5_868.8858699998536);
        allMarketsSummaries.Result.First()
            .Value.VolumeUsd.Should()
            .Be(5_868.8858699998536);
        allMarketsSummaries.Allowance.Cost.Should()
            .Be(0.015M);
        allMarketsSummaries.Allowance.Remaining.Should()
            .Be(9.985M);
        allMarketsSummaries.Allowance.RemainingPaid.Should()
            .Be(0);
        allMarketsSummaries.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
        allMarketsSummaries.Cursor.HasMore.Should()
            .BeFalse();
        allMarketsSummaries.Cursor.Last.Should()
            .Be("5Wx-YozvuveC1ViauOeZDwoxb7eHxgCD9PKtzf90l4YdJbrWpQm5nXlGozpGaA");
    }

    [Fact]
    public async Task Asserts_AllMarketsSummariesByIdFromCursorWithLimit_JsonResponseDeserialization()
    {
        const string keyBy = "id";
        const string cursor = "gCLPH49ToTXjmDnq2VyXCtu5HtSV__AiJU7XjdF1hAwu48pFj0_G70zdGrrKvg";
        const int limit = 3;
        _cryptoWatchServer.SetupUnauthenticatedAllMarketsSummariesByIdFromCursorWithLimitOf3RestEndpoint();

        var marketsSummaries =
            await new CryptoWatchApi(_httpClientFactory).Markets.SummariesAsync(keyBy, cursor, limit);

        marketsSummaries.Should()
            .BeOfType<Summaries>();
        marketsSummaries.Result.Should()
            .BeOfType<Dictionary<string, Summaries.ResultDetail>>()
            .Which.Should()
            .HaveCount(3);
        marketsSummaries.Result.Should()
            .ContainKey("3763781");
        marketsSummaries.Result.First()
            .Key.Should()
            .Be("3763626");
        marketsSummaries.Result.First()
            .Value.Price.Change.Absolute.Should()
            .Be(0.085700000000000109);
        marketsSummaries.Result.First()
            .Value.Price.Change.Percentage.Should()
            .Be(0.031662171648132453);
        marketsSummaries.Result.First()
            .Value.Price.High.Should()
            .Be(3.4790000000000001);
        marketsSummaries.Result.First()
            .Value.Price.Last.Should()
            .Be(2.7924000000000002);
        marketsSummaries.Result.First()
            .Value.Price.Low.Should()
            .Be(2.6789000000000001);
        marketsSummaries.Result.First()
            .Value.Volume.Should()
            .Be(1_813.2000000000428);
        marketsSummaries.Result.First()
            .Value.VolumeBase.Should()
            .Be(1_813.2000000000428);
        marketsSummaries.Result.First()
            .Value.VolumeQuote.Should()
            .Be(5_868.641839999852);
        marketsSummaries.Result.First()
            .Value.VolumeUsd.Should()
            .Be(5_868.641839999852);
        marketsSummaries.Allowance.Cost.Should()
            .Be(0.015M);
        marketsSummaries.Allowance.Remaining.Should()
            .Be(9.985M);
        marketsSummaries.Allowance.RemainingPaid.Should()
            .Be(0);
        marketsSummaries.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
        marketsSummaries.Cursor.HasMore.Should()
            .BeTrue();
        marketsSummaries.Cursor.Last.Should()
            .Be("KAfnAmma0L8uTRua-a0hBPAEasjMlP_3ervpj3oT5DbsGaXcdodYwksVi7PkAw");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOrderBook_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOrderBookRestEndpoint();

        var krakenBtcUsdOrderBook =
            await new CryptoWatchApi(_httpClientFactory).Markets.OrderBookAsync(exchange, pair);

        krakenBtcUsdOrderBook.Should()
            .BeOfType<OrderBook>();
        krakenBtcUsdOrderBook.Result.Asks.Should()
            .NotBeEmpty()
            .And
            .HaveCount(2)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29_380.900000000001, 0.022814609999999999 }));
        krakenBtcUsdOrderBook.Result.Bids.Should()
            .NotBeEmpty()
            .And
            .HaveCount(2)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29_380.299999999999, 4.6533219399999997 }));
        krakenBtcUsdOrderBook.Result.SequenceNumber.Should()
            .Be(28_187_578);
        krakenBtcUsdOrderBook.Allowance.Cost.Should()
            .Be(0.01M);
        krakenBtcUsdOrderBook.Allowance.Remaining.Should()
            .Be(9.99M);
        krakenBtcUsdOrderBook.Allowance.RemainingPaid.Should()
            .Be(0);
        krakenBtcUsdOrderBook.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOrderBookWithLimitOf3_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const uint limit = 3;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOrderBookWithLimitOf3RestEndpoint();

        var krakenBtcUsdOrderBook =
            await new CryptoWatchApi(_httpClientFactory).Markets.OrderBookAsync(exchange, pair, limit);

        krakenBtcUsdOrderBook.Should()
            .BeOfType<OrderBook>();
        krakenBtcUsdOrderBook.Result.Asks.Should()
            .NotBeEmpty()
            .And
            .HaveCount((int)limit)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29375.599999999999, 0.18446777 }));
        krakenBtcUsdOrderBook.Result.Bids.Should()
            .NotBeEmpty()
            .And
            .HaveCount((int)limit)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29374.700000000001, 0.087210889999999999 }));
        krakenBtcUsdOrderBook.Result.SequenceNumber.Should()
            .Be(28_194_781);
        krakenBtcUsdOrderBook.Allowance.Cost.Should()
            .Be(0.01M);
        krakenBtcUsdOrderBook.Allowance.Remaining.Should()
            .Be(9.99M);
        krakenBtcUsdOrderBook.Allowance.RemainingPaid.Should()
            .Be(0);
        krakenBtcUsdOrderBook.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOrderBookWithDepthOf60_000_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const double depth = 60_000;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOrderBookWithDepthOf60_000RestEndpoint();

        var krakenBtcUsdOrderBook =
            await new CryptoWatchApi(_httpClientFactory).Markets.OrderBookAsync(exchange, pair, depth);

        krakenBtcUsdOrderBook.Should()
            .BeOfType<OrderBook>();
        krakenBtcUsdOrderBook.Result.Asks.Should()
            .NotBeEmpty()
            .And
            .HaveCount(4)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29_207.099999999999, 0.02352125 }));
        krakenBtcUsdOrderBook.Result.Bids.Should()
            .NotBeEmpty()
            .And
            .HaveCount(4)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29_206.200000000001, 2.5679428799999999 }));
        krakenBtcUsdOrderBook.Result.SequenceNumber.Should()
            .Be(28_237_848);
        krakenBtcUsdOrderBook.Allowance.Cost.Should()
            .Be(0.01M);
        krakenBtcUsdOrderBook.Allowance.Remaining.Should()
            .Be(9.99M);
        krakenBtcUsdOrderBook.Allowance.RemainingPaid.Should()
            .Be(0);
        krakenBtcUsdOrderBook.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOrderBookWithDepthOf60_000AndLimitOf7_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const double depth = 60_000;
        const uint limit = 7;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOrderBookWithDepthOf60_000AndLimitOf7RestEndpoint();

        var krakenBtcUsdOrderBook =
            await new CryptoWatchApi(_httpClientFactory).Markets.OrderBookAsync(exchange, pair, depth, limit);

        krakenBtcUsdOrderBook.Should()
            .BeOfType<OrderBook>();
        krakenBtcUsdOrderBook.Result.Asks.Should()
            .NotBeEmpty()
            .And
            .HaveCount((int)limit)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29193.099999999999, 3.4254742600000001 }));
        krakenBtcUsdOrderBook.Result.Bids.Should()
            .NotBeEmpty()
            .And
            .HaveCount((int)limit)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29179, 0.00396 }));
        krakenBtcUsdOrderBook.Result.SequenceNumber.Should()
            .Be(28_239_179);
        krakenBtcUsdOrderBook.Allowance.Cost.Should()
            .Be(0.01M);
        krakenBtcUsdOrderBook.Allowance.Remaining.Should()
            .Be(9.99M);
        krakenBtcUsdOrderBook.Allowance.RemainingPaid.Should()
            .Be(0);
        krakenBtcUsdOrderBook.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOrderBookWithSpanOfDot875_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const decimal span = 0.875M;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOrderBookWithSpanOfDot875RestEndpoint();

        var krakenBtcUsdOrderBook =
            await new CryptoWatchApi(_httpClientFactory).Markets.OrderBookAsync(exchange, pair, span);

        krakenBtcUsdOrderBook.Should()
            .BeOfType<OrderBook>();
        krakenBtcUsdOrderBook.Result.Asks.Should()
            .NotBeEmpty()
            .And
            .HaveCount(195)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29_250.5, 10.256263369999999 }));
        krakenBtcUsdOrderBook.Result.Bids.Should()
            .NotBeEmpty()
            .And
            .HaveCount(249)
            .And
            .Contain(x => x.SequenceEqual(new[] { 28_976.900000000001, 0.0076563000000000004 }));
        krakenBtcUsdOrderBook.Result.SequenceNumber.Should()
            .Be(28_241_204);
        krakenBtcUsdOrderBook.Allowance.Cost.Should()
            .Be(0.01M);
        krakenBtcUsdOrderBook.Allowance.Remaining.Should()
            .Be(9.99M);
        krakenBtcUsdOrderBook.Allowance.RemainingPaid.Should()
            .Be(0);
        krakenBtcUsdOrderBook.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOrderBookWithSpanOfDot875AndLimitOf5_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const decimal span = 0.875M;
        const uint limit = 5;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOrderBookWithSpanOfDot875RestEndpoint();

        var krakenBtcUsdOrderBook =
            await new CryptoWatchApi(_httpClientFactory).Markets.OrderBookAsync(exchange, pair, span, limit);

        krakenBtcUsdOrderBook.Should()
            .BeOfType<OrderBook>();
        krakenBtcUsdOrderBook.Result.Asks.Should()
            .NotBeEmpty()
            .And
            .HaveCount(195)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29_250.5, 10.256263369999999 }));
        krakenBtcUsdOrderBook.Result.Bids.Should()
            .NotBeEmpty()
            .And
            .HaveCount(249)
            .And
            .Contain(x => x.SequenceEqual(new[] { 28_976.900000000001, 0.0076563000000000004 }));
        krakenBtcUsdOrderBook.Result.SequenceNumber.Should()
            .Be(28_241_204);
        krakenBtcUsdOrderBook.Allowance.Cost.Should()
            .Be(0.01M);
        krakenBtcUsdOrderBook.Allowance.Remaining.Should()
            .Be(9.99M);
        krakenBtcUsdOrderBook.Allowance.RemainingPaid.Should()
            .Be(0);
        krakenBtcUsdOrderBook.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOrderBookWithSpanOfDot875AndDepthOf13_000_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const double depth = 13_000;
        const decimal span = 0.875M;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOrderBookWithSpanOfDot875AndDepthOf13_000RestEndpoint();

        var krakenBtcUsdOrderBook =
            await new CryptoWatchApi(_httpClientFactory).Markets.OrderBookAsync(exchange, pair, depth, span);

        krakenBtcUsdOrderBook.Should()
            .BeOfType<OrderBook>();
        krakenBtcUsdOrderBook.Result.Asks.Should()
            .NotBeEmpty()
            .And
            .HaveCount(204)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29_283.099999999999, 4.0499999999999998 }));
        krakenBtcUsdOrderBook.Result.Bids.Should()
            .NotBeEmpty()
            .And
            .HaveCount(244)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29_033.400000000001, 0.0001 }));
        krakenBtcUsdOrderBook.Result.SequenceNumber.Should()
            .Be(28_246_239);
        krakenBtcUsdOrderBook.Allowance.Cost.Should()
            .Be(0.01M);
        krakenBtcUsdOrderBook.Allowance.Remaining.Should()
            .Be(9.99M);
        krakenBtcUsdOrderBook.Allowance.RemainingPaid.Should()
            .Be(0);
        krakenBtcUsdOrderBook.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task
        Asserts_KrakenBtcUsdOrderBookWithSpanOfDot875AndDepthOf13_000AndLimitOf11_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const double depth = 13_000;
        const decimal span = 0.875M;
        const uint limit = 11;
        _cryptoWatchServer
            .SetupUnauthenticatedKrakenUsdBtcOrderBookWithSpanOfDot875AndDepthOf13AndLimitOf11_000RestEndpoint();

        var krakenBtcUsdOrderBook =
            await new CryptoWatchApi(_httpClientFactory).Markets.OrderBookAsync(exchange, pair, depth, span,
                limit);

        krakenBtcUsdOrderBook.Should()
            .BeOfType<OrderBook>();
        krakenBtcUsdOrderBook.Result.Asks.Should()
            .NotBeEmpty()
            .And
            .HaveCount(11)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29_157.700000000001, 0.0051061199999999996 }));
        krakenBtcUsdOrderBook.Result.Bids.Should()
            .NotBeEmpty()
            .And
            .HaveCount(11)
            .And
            .Contain(x => x.SequenceEqual(new[] { 29_154.099999999999, 0.72060049000000004 }));
        krakenBtcUsdOrderBook.Result.SequenceNumber.Should()
            .Be(28_247_929);
        krakenBtcUsdOrderBook.Allowance.Cost.Should()
            .Be(0.01M);
        krakenBtcUsdOrderBook.Allowance.Remaining.Should()
            .Be(9.99M);
        krakenBtcUsdOrderBook.Allowance.RemainingPaid.Should()
            .Be(0);
        krakenBtcUsdOrderBook.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOrderBookLiquidity_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOrderBookLiquidity_000RestEndpoint();

        var krakenBtcUsdOrderBookLiquidity =
            await new CryptoWatchApi(_httpClientFactory).Markets.OrderBookLiquidityAsync(exchange, pair);

        krakenBtcUsdOrderBookLiquidity.Should()
            .BeOfType<OrderBookLiquidity>();
        krakenBtcUsdOrderBookLiquidity.Result.Should()
            .BeOfType<OrderBookLiquidity.ResultDetails>();
        krakenBtcUsdOrderBookLiquidity.Result.Asks.Should()
            .BeOfType<Dictionary<string, Dictionary<int, double>>>()
            .And.HaveCount(2)
            .And.ContainKeys("base", "quote")
            .And.Subject.Values.Should()
            .Subject.SelectMany(x => x)
            .Should()
            .Contain(x => x.Key == 300 && Math.Abs(x.Value - 672.97948774999998) < 0.0000000001)
            .And
            .Contain(x => x.Key == 400 && Math.Abs(x.Value - 21_255_370.468907736) < 0.0000000001);
        krakenBtcUsdOrderBookLiquidity.Result.Bids.Should()
            .BeOfType<Dictionary<string, Dictionary<int, double>>>()
            .And.HaveCount(2)
            .And.ContainKeys("base", "quote")
            .And.Subject.Values.Should()
            .Subject.SelectMany(x => x)
            .Should()
            .Contain(x => x.Key == 300 && Math.Abs(x.Value - 518.01191123000001) < 0.0000000001)
            .And
            .Contain(x => x.Key == 400 && Math.Abs(x.Value - 19_399_484.588133283) < 0.0000000001);
        krakenBtcUsdOrderBookLiquidity.Allowance.Cost.Should()
            .Be(0.005M);
        krakenBtcUsdOrderBookLiquidity.Allowance.Remaining.Should()
            .Be(9.995M);
        krakenBtcUsdOrderBookLiquidity.Allowance.RemainingPaid.Should()
            .Be(0);
        krakenBtcUsdOrderBookLiquidity.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOrderBookCalculator_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const double amount = 3.7;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOrderBookCalculator_000RestEndpoint();

        var orderBookCalculatorAsync =
            await new CryptoWatchApi(_httpClientFactory).Markets
                .OrderBookCalculatorAsync(exchange, pair, amount);

        orderBookCalculatorAsync.Should()
            .BeOfType<OrderBookCalculator>()
            .Subject.Result.Should()
            .BeOfType<OrderBookCalculator.ResultDetail>();
        orderBookCalculatorAsync.Result.Buy.Should()
            .BeOfType<OrderBookCalculator.BuyTransaction>();
        orderBookCalculatorAsync.Result.Sell.Should()
            .BeOfType<OrderBookCalculator.SellTransaction>();
        orderBookCalculatorAsync.Result.Buy.AverageBps.Should()
            .Be(1);
        orderBookCalculatorAsync.Result.Buy.AverageDelta.Should()
            .Be(3.8496747256756758);
        orderBookCalculatorAsync.Result.Buy.AveragePrice.Should()
            .Be(29_462.149674725675);
        orderBookCalculatorAsync.Result.Buy.ReachDelta.Should()
            .Be(5.9000000000000004);
        orderBookCalculatorAsync.Result.Buy.ReachDeltaBps.Should()
            .Be(2);
        orderBookCalculatorAsync.Result.Buy.ReachPrice.Should()
            .Be(29_464.200000000001);
        orderBookCalculatorAsync.Result.Buy.Spend.Should()
            .Be(109_009.953796485);
        orderBookCalculatorAsync.Result.Sell.AverageBps.Should()
            .Be(0);
        orderBookCalculatorAsync.Result.Sell.AverageDelta.Should()
            .Be(0);
        orderBookCalculatorAsync.Result.Sell.AveragePrice.Should()
            .Be(29458.200000000001);
        orderBookCalculatorAsync.Result.Sell.ReachDelta.Should()
            .Be(0);
        orderBookCalculatorAsync.Result.Sell.ReachDeltaBps.Should()
            .Be(0);
        orderBookCalculatorAsync.Result.Sell.ReachPrice.Should()
            .Be(29_458.200000000001);
        orderBookCalculatorAsync.Result.Sell.Receive.Should()
            .Be(108_995.34);
        orderBookCalculatorAsync.Allowance.Cost.Should()
            .Be(0.015M);
        orderBookCalculatorAsync.Allowance.Remaining.Should()
            .Be(9.985M);
        orderBookCalculatorAsync.Allowance.RemainingPaid.Should()
            .Be(0);
        orderBookCalculatorAsync.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOHLCCandlestick_JsonDeserializationResponse()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOHLCRestEndpoint();

        var ohlcCandlesticks =
            await new CryptoWatchApi(_httpClientFactory).Markets.OHLCCandlesticksAsync(exchange, pair);

        ohlcCandlesticks.Should()
            .BeOfType<CandlestickHistories>();
        ohlcCandlesticks.Result.Should()
            .HaveCount(14)
            .And.BeOfType<Dictionary<string, double[][]>>()
            .And.ContainKeys(
                Enum.GetValuesAsUnderlyingType<TimeFrame>()
                    .Cast<int>()
                    .Select(x => x.ToString())
            );
        ohlcCandlesticks.TimeBasedCandlestickHistories.Should()
            .HaveCount(14)
            .And.BeOfType<CandlestickHistories.TimeBasedCandlestickHistory[]>()
            .And.AllSatisfy(x =>
            {
                var candleTimeDelta = TimeSpan.FromSeconds((int)x.TimeFrame);
                var closeTime = x.OpenHighLowCloseCandles.First()
                    .CloseTime;
                foreach (var openHighLowCloseCandle in x.OpenHighLowCloseCandles.Skip(1))
                {
                    openHighLowCloseCandle.CloseTime.Subtract(closeTime)
                        .Should()
                        .Be(candleTimeDelta);
                    closeTime = openHighLowCloseCandle.CloseTime;
                }
            });
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.OpenPrice)
            .Should()
            .Be(24588.75);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.HighPrice)
            .Should()
            .Be(24875.25);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.LowPrice)
            .Should()
            .Be(24411.2);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.ClosePrice)
            .Should()
            .Be(24821.2);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.Volume)
            .Should()
            .Be(793.150604525);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.QuoteVolume)
            .Should()
            .Be(19526849.608053513);
        ohlcCandlesticks.Allowance.Cost.Should()
            .Be(0.015M);
        ohlcCandlesticks.Allowance.Remaining.Should()
            .Be(9.985M);
        ohlcCandlesticks.Allowance.RemainingPaid.Should()
            .Be(0);
        ohlcCandlesticks.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdFiveMinuteOHLCCandlestick_JsonDeserializationResponse()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const TimeFrame timeFrame = TimeFrame.min5;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcFiveMinuteOHLCRestEndpoint();

        var ohlcCandlesticks =
            await new CryptoWatchApi(_httpClientFactory).Markets
                .OHLCCandlesticksAsync(exchange, pair, timeFrame);

        ohlcCandlesticks.Should()
            .BeOfType<CandlestickHistories>();
        ohlcCandlesticks.TimeBasedCandlestickHistories.Should()
            .HaveCount(1)
            .And.BeOfType<CandlestickHistories.TimeBasedCandlestickHistory[]>()
            .And.AllSatisfy(x =>
            {
                var candleTimeDelta = TimeSpan.FromSeconds((int)x.TimeFrame);
                var closeTime = x.OpenHighLowCloseCandles.First()
                    .CloseTime;
                foreach (var openHighLowCloseCandle in x.OpenHighLowCloseCandles.Skip(1))
                {
                    openHighLowCloseCandle.CloseTime.Subtract(closeTime)
                        .Should()
                        .Be(candleTimeDelta);
                    closeTime = openHighLowCloseCandle.CloseTime;
                }
            });
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.OpenPrice)
            .Should()
            .Be(29203.841935483877);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.HighPrice)
            .Should()
            .Be(29216.396774193552);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.LowPrice)
            .Should()
            .Be(29196.916129032255);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.ClosePrice)
            .Should()
            .Be(29207.245161290324);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.Volume)
            .Should()
            .Be(5.752224367741936);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.QuoteVolume)
            .Should()
            .Be(168152.674565141);
        ohlcCandlesticks.Allowance.Cost.Should()
            .Be(0.015M);
        ohlcCandlesticks.Allowance.Remaining.Should()
            .Be(9.985M);
        ohlcCandlesticks.Allowance.RemainingPaid.Should()
            .Be(0);
        ohlcCandlesticks.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOHLCCandlestickAfter01_01_2023Long_JsonDeserializationResponse()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const int after = 1672610151;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOHLCAfter01_01_2023RestEndpoint();

        var ohlcCandlesticks =
            await new CryptoWatchApi(_httpClientFactory).Markets.OHLCCandlesticksAsync(exchange, pair, after);

        ohlcCandlesticks.Should()
            .BeOfType<CandlestickHistories>();
        ohlcCandlesticks.Result.Should()
            .HaveCount(14)
            .And.BeOfType<Dictionary<string, double[][]>>()
            .And.ContainKeys(
                Enum.GetValuesAsUnderlyingType<TimeFrame>()
                    .Cast<int>()
                    .Select(x => x.ToString())
            );
        ohlcCandlesticks.TimeBasedCandlestickHistories.Should()
            .HaveCount(14)
            .And.BeOfType<CandlestickHistories.TimeBasedCandlestickHistory[]>()
            .And.AllSatisfy(x =>
            {
                var candleTimeDelta = TimeSpan.FromSeconds((int)x.TimeFrame);
                var closeTime = x.OpenHighLowCloseCandles.First()
                    .CloseTime;
                foreach (var openHighLowCloseCandle in x.OpenHighLowCloseCandles.Skip(1))
                {
                    openHighLowCloseCandle.CloseTime.Subtract(closeTime)
                        .Should()
                        .Be(candleTimeDelta);
                    closeTime = openHighLowCloseCandle.CloseTime;
                }
            });
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.OpenPrice)
            .Should()
            .Be(16772.121875);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.HighPrice)
            .Should()
            .Be(16820.000000000004);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.LowPrice)
            .Should()
            .Be(16729.434374999997);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.ClosePrice)
            .Should()
            .Be(16783.246875);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.Volume)
            .Should()
            .Be(288.22511906281244);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.QuoteVolume)
            .Should()
            .Be(4836929.181191225);
        ohlcCandlesticks.Allowance.Cost.Should()
            .Be(0.015M);
        ohlcCandlesticks.Allowance.Remaining.Should()
            .Be(9.985M);
        ohlcCandlesticks.Allowance.RemainingPaid.Should()
            .Be(0);
        ohlcCandlesticks.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOHLCCandlestickAfter01_01_2023DateTimeOffset_JsonDeserializationResponse()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        var after = DateTimeOffset.FromUnixTimeSeconds(1672610151);
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOHLCAfter01_01_2023RestEndpoint();

        var ohlcCandlesticks =
            await new CryptoWatchApi(_httpClientFactory).Markets.OHLCCandlesticksAsync(exchange, pair, after);

        ohlcCandlesticks.Should()
            .BeOfType<CandlestickHistories>();
        ohlcCandlesticks.Result.Should()
            .HaveCount(14)
            .And.BeOfType<Dictionary<string, double[][]>>()
            .And.ContainKeys(
                Enum.GetValuesAsUnderlyingType<TimeFrame>()
                    .Cast<int>()
                    .Select(x => x.ToString())
            );
        ohlcCandlesticks.TimeBasedCandlestickHistories.Should()
            .HaveCount(14)
            .And.BeOfType<CandlestickHistories.TimeBasedCandlestickHistory[]>()
            .And.AllSatisfy(x =>
            {
                var candleTimeDelta = TimeSpan.FromSeconds((int)x.TimeFrame);
                var closeTime = x.OpenHighLowCloseCandles.First()
                    .CloseTime;
                foreach (var openHighLowCloseCandle in x.OpenHighLowCloseCandles.Skip(1))
                {
                    openHighLowCloseCandle.CloseTime.Subtract(closeTime)
                        .Should()
                        .Be(candleTimeDelta);
                    closeTime = openHighLowCloseCandle.CloseTime;
                }
            });
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.OpenPrice)
            .Should()
            .Be(16772.121875);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.HighPrice)
            .Should()
            .Be(16820.000000000004);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.LowPrice)
            .Should()
            .Be(16729.434374999997);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.ClosePrice)
            .Should()
            .Be(16783.246875);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.Volume)
            .Should()
            .Be(288.22511906281244);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.QuoteVolume)
            .Should()
            .Be(4836929.181191225);
        ohlcCandlesticks.Allowance.Cost.Should()
            .Be(0.015M);
        ohlcCandlesticks.Allowance.Remaining.Should()
            .Be(9.985M);
        ohlcCandlesticks.Allowance.RemainingPaid.Should()
            .Be(0);
        ohlcCandlesticks.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdOneMinuteOHLCCandlestickAfter01_01_2023Long_JsonDeserializationResponse()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const int after = 1672610151;
        const TimeFrame timeFrame = TimeFrame.min1;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOneMinuteOHLCAfter01_01_2023RestEndpoint();

        var ohlcCandlesticks =
            await new CryptoWatchApi(_httpClientFactory).Markets.OHLCCandlesticksAsync(exchange, pair, after,
                timeFrame);

        ohlcCandlesticks.Should()
            .BeOfType<CandlestickHistories>();
        ohlcCandlesticks.TimeBasedCandlestickHistories.Should()
            .HaveCount(1)
            .And.BeOfType<CandlestickHistories.TimeBasedCandlestickHistory[]>()
            .And.AllSatisfy(x =>
            {
                var candleTimeDelta = TimeSpan.FromSeconds((int)x.TimeFrame);
                var closeTime = x.OpenHighLowCloseCandles.First()
                    .CloseTime;
                foreach (var openHighLowCloseCandle in x.OpenHighLowCloseCandles.Skip(1))
                {
                    openHighLowCloseCandle.CloseTime.Subtract(closeTime)
                        .Should()
                        .Be(candleTimeDelta);
                    closeTime = openHighLowCloseCandle.CloseTime;
                }
            });
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.OpenPrice)
            .Should()
            .Be(29156.707692307693);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.HighPrice)
            .Should()
            .Be(29158.823076923083);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.LowPrice)
            .Should()
            .Be(29156.553846153845);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.ClosePrice)
            .Should()
            .Be(29158.59230769231);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.Volume)
            .Should()
            .Be(4.610501832307692);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.QuoteVolume)
            .Should()
            .Be(134378.79769660032);
        ohlcCandlesticks.Allowance.Cost.Should()
            .Be(0.015M);
        ohlcCandlesticks.Allowance.Remaining.Should()
            .Be(9.985M);
        ohlcCandlesticks.Allowance.RemainingPaid.Should()
            .Be(0);
        ohlcCandlesticks.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task
        Asserts_KrakenBtcUsdOneMinuteOHLCCandlestickAfter01_01_2023DateTimeOffset_JsonDeserializationResponse()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        var after = DateTimeOffset.FromUnixTimeSeconds(1672610151);
        const TimeFrame timeFrame = TimeFrame.min1;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOneMinuteOHLCAfter01_01_2023RestEndpoint();

        var ohlcCandlesticks =
            await new CryptoWatchApi(_httpClientFactory).Markets.OHLCCandlesticksAsync(exchange, pair, after,
                timeFrame);

        ohlcCandlesticks.Should()
            .BeOfType<CandlestickHistories>();
        ohlcCandlesticks.TimeBasedCandlestickHistories.Should()
            .HaveCount(1)
            .And.BeOfType<CandlestickHistories.TimeBasedCandlestickHistory[]>()
            .And.AllSatisfy(x =>
            {
                var candleTimeDelta = TimeSpan.FromSeconds((int)x.TimeFrame);
                var closeTime = x.OpenHighLowCloseCandles.First()
                    .CloseTime;
                foreach (var openHighLowCloseCandle in x.OpenHighLowCloseCandles.Skip(1))
                {
                    openHighLowCloseCandle.CloseTime.Subtract(closeTime)
                        .Should()
                        .Be(candleTimeDelta);
                    closeTime = openHighLowCloseCandle.CloseTime;
                }
            });
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.OpenPrice)
            .Should()
            .Be(29156.707692307693);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.HighPrice)
            .Should()
            .Be(29158.823076923083);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.LowPrice)
            .Should()
            .Be(29156.553846153845);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.ClosePrice)
            .Should()
            .Be(29158.59230769231);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.Volume)
            .Should()
            .Be(4.610501832307692);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.QuoteVolume)
            .Should()
            .Be(134378.79769660032);
        ohlcCandlesticks.Allowance.Cost.Should()
            .Be(0.015M);
        ohlcCandlesticks.Allowance.Remaining.Should()
            .Be(9.985M);
        ohlcCandlesticks.Allowance.RemainingPaid.Should()
            .Be(0);
        ohlcCandlesticks.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task
        Asserts_KrakenBtcUsdOneHourOHLCCandlestickAfter01_01_2023Before02_01_2023Long_JsonDeserializationResponse()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const int after = 1672531200;
        const int before = 1672617600;
        const TimeFrame timeFrame = TimeFrame.h1;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOneHourOHLCAfter01_01_2023Before02_01_2023RestEndpoint();

        var ohlcCandlesticks =
            await new CryptoWatchApi(_httpClientFactory).Markets.OHLCCandlesticksAsync(exchange, pair, before,
                after, timeFrame);

        ohlcCandlesticks.Should()
            .BeOfType<CandlestickHistories>();
        ohlcCandlesticks.TimeBasedCandlestickHistories.Should()
            .HaveCount(1)
            .And.BeOfType<CandlestickHistories.TimeBasedCandlestickHistory[]>()
            .And.AllSatisfy(x =>
            {
                var candleTimeDelta = TimeSpan.FromSeconds((int)x.TimeFrame);
                var closeTime = x.OpenHighLowCloseCandles.First()
                    .CloseTime;
                foreach (var openHighLowCloseCandle in x.OpenHighLowCloseCandles.Skip(1))
                {
                    openHighLowCloseCandle.CloseTime.Subtract(closeTime)
                        .Should()
                        .Be(candleTimeDelta);
                    closeTime = openHighLowCloseCandle.CloseTime;
                }
            });
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.OpenPrice)
            .Should()
            .Be(16553.756);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.HighPrice)
            .Should()
            .Be(16565.036);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.LowPrice)
            .Should()
            .Be(16542.56);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.ClosePrice)
            .Should()
            .Be(16555.168);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.Volume)
            .Should()
            .Be(50.8506891136);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.QuoteVolume)
            .Should()
            .Be(841059.8006089081);
        ohlcCandlesticks.Allowance.Cost.Should()
            .Be(0.015M);
        ohlcCandlesticks.Allowance.Remaining.Should()
            .Be(9.985M);
        ohlcCandlesticks.Allowance.RemainingPaid.Should()
            .Be(0);
        ohlcCandlesticks.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task
        Asserts_KrakenBtcUsdOneHourOHLCCandlestickAfter01_01_2023Before02_01_2023DateTimeOffset_JsonDeserializationResponse()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        var after =
            new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(1672531200);
        var before = 
            new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(1672617600);
        const TimeFrame timeFrame = TimeFrame.h1;
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcOneHourOHLCAfter01_01_2023Before02_01_2023RestEndpoint();

        var ohlcCandlesticks =
            await new CryptoWatchApi(_httpClientFactory).Markets.OHLCCandlesticksAsync(exchange, pair, before,
                after, timeFrame);

        ohlcCandlesticks.Should()
            .BeOfType<CandlestickHistories>();
        ohlcCandlesticks.TimeBasedCandlestickHistories.Should()
            .HaveCount(1)
            .And.BeOfType<CandlestickHistories.TimeBasedCandlestickHistory[]>()
            .And.AllSatisfy(x =>
            {
                var candleTimeDelta = TimeSpan.FromSeconds((int)x.TimeFrame);
                var closeTime = x.OpenHighLowCloseCandles.First()
                    .CloseTime;
                foreach (var openHighLowCloseCandle in x.OpenHighLowCloseCandles.Skip(1))
                {
                    openHighLowCloseCandle.CloseTime.Subtract(closeTime)
                        .Should()
                        .Be(candleTimeDelta);
                    closeTime = openHighLowCloseCandle.CloseTime;
                }
            });
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.OpenPrice)
            .Should()
            .Be(16553.756);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.HighPrice)
            .Should()
            .Be(16565.036);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.LowPrice)
            .Should()
            .Be(16542.56);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.ClosePrice)
            .Should()
            .Be(16555.168);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.Volume)
            .Should()
            .Be(50.8506891136);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.QuoteVolume)
            .Should()
            .Be(841059.8006089081);
        ohlcCandlesticks.Allowance.Cost.Should()
            .Be(0.015M);
        ohlcCandlesticks.Allowance.Remaining.Should()
            .Be(9.985M);
        ohlcCandlesticks.Allowance.RemainingPaid.Should()
            .Be(0);
        ohlcCandlesticks.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_KrakenBtcUsdFiveMinuteTwoHourOHLCCandlestick_JsonDeserializationResponse()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        var timeFrame = new[] { TimeFrame.min5, TimeFrame.h2 };
        _cryptoWatchServer.SetupUnauthenticatedKrakenUsdBtcFiveMinuteTwoHourOHLCRestEndpoint();

        var ohlcCandlesticks =
            await new CryptoWatchApi(_httpClientFactory).Markets.OHLCCandlesticksAsync(exchange, pair, timeFrame);

        ohlcCandlesticks.Should()
            .BeOfType<CandlestickHistories>();
        ohlcCandlesticks.TimeBasedCandlestickHistories.Should()
            .Match(x => x.All(y => y.TimeFrame == TimeFrame.min5 || y.TimeFrame == TimeFrame.h2))
            .And
            .HaveCount(2)
            .And.BeOfType<CandlestickHistories.TimeBasedCandlestickHistory[]>()
            .And.AllSatisfy(x =>
            {
                var candleTimeDelta = TimeSpan.FromSeconds((int)x.TimeFrame);
                var closeTime = x.OpenHighLowCloseCandles.First()
                    .CloseTime;
                foreach (var openHighLowCloseCandle in x.OpenHighLowCloseCandles.Skip(1))
                {
                    openHighLowCloseCandle.CloseTime.Subtract(closeTime)
                        .Should()
                        .Be(candleTimeDelta);
                    closeTime = openHighLowCloseCandle.CloseTime;
                }
            });
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.OpenPrice)
            .Should()
            .Be(28988.40769230769);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.HighPrice)
            .Should()
            .Be(28991.684615384616);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.LowPrice)
            .Should()
            .Be(28984.792307692307);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.ClosePrice)
            .Should()
            .Be(28988.59230769231);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.Volume)
            .Should()
            .Be(1.4770392142307693);
        ohlcCandlesticks.TimeBasedCandlestickHistories[0]
            .OpenHighLowCloseCandles.Average(x => x.QuoteVolume)
            .Should()
            .Be(42815.55593027423);
        ohlcCandlesticks.Allowance.Cost.Should()
            .Be(0.015M);
        ohlcCandlesticks.Allowance.Remaining.Should()
            .Be(9.985M);
        ohlcCandlesticks.Allowance.RemainingPaid.Should()
            .Be(0);
        ohlcCandlesticks.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_InvalidKrakenBtcUsdOHLCCandlestick_JsonDeserializationResponse()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        const TimeFrame timeFrame = TimeFrame.min5;
        _cryptoWatchServer.SetupUnauthenticatedInvalidKrakenUsdBtcOHLCRestEndpoint();

        var ohlcCandlesticks =
            await new CryptoWatchApi(_httpClientFactory).Markets.OHLCCandlesticksAsync(exchange, pair, timeFrame);
        var x = () => ohlcCandlesticks.TimeBasedCandlestickHistories;

        ohlcCandlesticks.Should()
            .BeOfType<CandlestickHistories>();
        x.Should()
            .ThrowExactly<InvalidEnumArgumentException>();
    }
}
