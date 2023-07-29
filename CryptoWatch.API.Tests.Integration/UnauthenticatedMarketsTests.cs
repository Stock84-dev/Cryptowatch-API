using CryptoWatch.API.Types;
using FluentAssertions;
using Moq;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public class UnauthenticatedMarketsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();

    public UnauthenticatedMarketsTests()
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
    public async Task Asserts_MarketsListing_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedDefaultListingMarketsRestEndpoint();

        var marketListing = await new CryptoWatchApi(_httpClientFactory.Object).Markets.ListAsync();

        marketListing.Should()
            .BeOfType<MarketCollection>();
        marketListing.Result.Should()
            .BeOfType<List<MarketDetails>>();
        marketListing.Result.First()
            .Should()
            .BeOfType<MarketDetails>();
        marketListing.Result.Should()
            .HaveCount(20_000);
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

        var marketListing = await new CryptoWatchApi(_httpClientFactory.Object).Markets.ListAsync(cursor);

        marketListing.Should()
            .BeOfType<MarketCollection>();
        marketListing.Result.Should()
            .BeOfType<List<MarketDetails>>();
        marketListing.Result.First()
            .Should()
            .BeOfType<MarketDetails>();
        marketListing.Result.Should()
            .HaveCount(7_651);
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

        var marketListing = await new CryptoWatchApi(_httpClientFactory.Object).Markets.ListAsync(items);

        marketListing.Should()
            .BeOfType<MarketCollection>();
        marketListing.Result.Should()
            .BeOfType<List<MarketDetails>>();
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

        var marketListing = await new CryptoWatchApi(_httpClientFactory.Object).Markets.ListAsync(cursor, item);

        marketListing.Should()
            .BeOfType<MarketCollection>();
        marketListing.Result.Should()
            .BeOfType<List<MarketDetails>>();
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

        var marketPairDetail = await new CryptoWatchApi(_httpClientFactory.Object).Markets.DetailsAsync(exchange, pair);

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

        var marketsPrices = await new CryptoWatchApi(_httpClientFactory.Object).Markets.PriceAsync();

        marketsPrices.Should()
            .BeOfType<MarketPrices>();
        marketsPrices.Result.Should()
            .BeOfType<Dictionary<string, decimal>>();
        marketsPrices.Result.Should()
            .HaveCount(14_054);
        marketsPrices.Result.Should()
            .ContainKey("market:binance-us:aptusdt");
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

        var marketsPrices = await new CryptoWatchApi(_httpClientFactory.Object).Markets.PriceAsync(cursor);

        marketsPrices.Should()
            .BeOfType<MarketPrices>();
        marketsPrices.Result.Should()
            .BeOfType<Dictionary<string, decimal>>();
        marketsPrices.Result.Should()
            .HaveCount(14_060);
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

        var marketsPrices = await new CryptoWatchApi(_httpClientFactory.Object).Markets.PriceAsync(cursor, items);

        marketsPrices.Should()
            .BeOfType<MarketPrices>();
        marketsPrices.Result.Should()
            .BeOfType<Dictionary<string, decimal>>();
        marketsPrices.Result.Should()
            .HaveCount(14_061);
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

        var marketPairPrice = await new CryptoWatchApi(_httpClientFactory.Object).Markets.PriceAsync(exchange, pair);

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
    public void Asserts_MostRecentTrades_TypeConsistency()
    {
        typeof(MostRecentTrades).Should()
            .NotHaveDefaultConstructor();
        typeof(MostRecentTrades).Should()
            .HaveProperty<List<RecentTrade>>(nameof(MostRecentTrades.RecentTrades))
            .Which.Should()
            .NotBeWritable();
        typeof(MostRecentTrades).Should()
            .HaveProperty<Allowance>(nameof(MostRecentTrades.Allowance))
            .Which.Should()
            .NotBeWritable();
    }

    [Fact]
    public async Task Asserts_MostRecentTrades_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        _cryptoWatchServer.SetupUnauthenticatedMarketMostRecentTradesOfAPairRestEndpoint();

        var mostRecentTrades = await new CryptoWatchApi(_httpClientFactory.Object).Markets.TradesAsync(exchange, pair);

        mostRecentTrades.Should()
            .BeOfType<MostRecentTrades>();
        mostRecentTrades.Result.Should()
            .BeOfType<List<List<decimal>>>();
        mostRecentTrades.RecentTrades.Should()
            .BeOfType<List<RecentTrade>>();
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
            await new CryptoWatchApi(_httpClientFactory.Object).Markets.TradesAsync(exchange, pair, cursor);

        mostRecentTrades.Should()
            .BeOfType<MostRecentTrades>();
        mostRecentTrades.Result.Should()
            .BeOfType<List<List<decimal>>>();
        mostRecentTrades.RecentTrades.Should()
            .BeOfType<List<RecentTrade>>();
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
            await new CryptoWatchApi(_httpClientFactory.Object).Markets.TradesAsync(exchange, pair, cursor, limit);

        mostRecentTrades.Should()
            .BeOfType<MostRecentTrades>();
        mostRecentTrades.Result.Should()
            .BeOfType<List<List<decimal>>>();
        mostRecentTrades.RecentTrades.Should()
            .BeOfType<List<RecentTrade>>();
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
    public void Asserts_Summary_TypeConsistency()
    {
        typeof(Summary).Should()
            .NotHaveDefaultConstructor();
        typeof(Summary).Should()
            .HaveProperty<Summary.ResultDetail>(nameof(Summary.Result))
            .Which.Should()
            .NotBeWritable();
        typeof(Summary.ResultDetail).Should()
            .HaveProperty<Price>(nameof(Summary.ResultDetail.Price))
            .Which.Should()
            .NotBeWritable();
        typeof(Summary.ResultDetail).Should()
            .HaveProperty<double>(nameof(Summary.ResultDetail.Volume))
            .Which.Should()
            .NotBeWritable();
        typeof(Summary.ResultDetail).Should()
            .HaveProperty<double>(nameof(Summary.ResultDetail.VolumeQuote))
            .Which.Should()
            .NotBeWritable();
        typeof(Summary).Should()
            .HaveProperty<Allowance>(nameof(Summary.Allowance))
            .Which.Should()
            .NotBeWritable();
    }

    [Fact]
    public async Task Asserts_MarketPairSummary_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        const string pair = "btcusd";
        _cryptoWatchServer.SetupUnauthenticatedMarketPairSummaryRestEndpoint();

        var marketPairSummary =
            await new CryptoWatchApi(_httpClientFactory.Object).Markets.SummaryAsync(exchange, pair);

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
    public void Asserts_AllMarketsSummaries_TypeConsistency()
    {
        typeof(Summaries).Should()
            .NotHaveDefaultConstructor();
        typeof(Summaries).Should()
            .HaveProperty<Dictionary<string, Summaries.ResultDetail>>(nameof(Summaries.Result))
            .Which.Should()
            .NotBeWritable();
        typeof(Summaries.ResultDetail).Should()
            .HaveProperty<Price>(nameof(Summaries.ResultDetail.Price))
            .Which.Should()
            .NotBeWritable();
        typeof(Summaries.ResultDetail).Should()
            .HaveProperty<double>(nameof(Summaries.ResultDetail.Volume))
            .Which.Should()
            .NotBeWritable();
        typeof(Summaries.ResultDetail).Should()
            .HaveProperty<double>(nameof(Summaries.ResultDetail.VolumeBase))
            .Which.Should()
            .NotBeWritable();
        typeof(Summaries.ResultDetail).Should()
            .HaveProperty<double>(nameof(Summaries.ResultDetail.VolumeQuote))
            .Which.Should()
            .NotBeWritable();
        typeof(Summaries.ResultDetail).Should()
            .HaveProperty<double>(nameof(Summaries.ResultDetail.VolumeUsd))
            .Which.Should()
            .NotBeWritable();
    }

    [Fact]
    public async Task Asserts_AllMarketsSummaries_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedAllMarketsSummariesRestEndpoint();

        var allMarketsSummaries = await new CryptoWatchApi(_httpClientFactory.Object).Markets.SummariesAsync();

        allMarketsSummaries.Should()
            .BeOfType<Summaries>();
        allMarketsSummaries.Result.Should()
            .BeOfType<Dictionary<string, Summaries.ResultDetail>>()
            .Which.Should()
            .HaveCount(13_928);
        allMarketsSummaries.Result.Should().ContainKey("binance-us:icpusdt");
        allMarketsSummaries.Result.First().Key.Should()
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
        allMarketsSummaries.Result.First().Value.Volume.Should()
            .Be(1_440.4999999999627);
        allMarketsSummaries.Result.First()
            .Value.VolumeBase.Should()
            .Be(1_440.4999999999627);
        allMarketsSummaries.Result.First().Value.VolumeQuote.Should()
            .Be(437.85069999994812);
        allMarketsSummaries.Result.First()
            .Value.VolumeUsd.Should()
            .Be(437.86940701357082);
        allMarketsSummaries.Allowance.Cost.Should().Be(0.015M);
        allMarketsSummaries.Allowance.Remaining.Should().Be(9.985M);
        allMarketsSummaries.Allowance.RemainingPaid.Should().Be(0);
        allMarketsSummaries.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }
}
