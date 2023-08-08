using System.Net.Http.Json;
using System.Text;
using CryptoWatch.API.Types;

namespace CryptoWatch.API.Paths;

public readonly struct MarketsApi
{
    private const string Route = "/markets";
    private readonly IHttpClientFactory _httpClientFactory;

    internal MarketsApi(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    public Task<HttpResponseMessage> ExchangeAsync(string exchange) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}");

    public Task<MarketCollection> ListAsync() =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MarketCollection>(Route);

    public Task<MarketCollection> ListAsync(string cursor) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MarketCollection>($"{Route}?cursor={cursor}");

    public Task<MarketCollection> ListAsync(uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MarketCollection>($"{Route}?limit={limit}");

    public Task<MarketCollection> ListAsync(string cursor, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MarketCollection>($"{Route}?cursor={cursor}&limit={limit}");

    public Task<MarketPairDetail> DetailsAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MarketPairDetail>($"{Route}/{exchange}/{pair}");

    public Task<MarketPrices> PriceAsync() =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MarketPrices>($"{Route}/prices");

    public Task<MarketPrices> PriceAsync(string cursor) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MarketPrices>($"{Route}/prices?cursor={cursor}");

    public Task<MarketPrices> PriceAsync(string cursor, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MarketPrices>($"{Route}/prices?cursor={cursor}&limit={limit}");

    public Task<MarketPairPrice> PriceAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MarketPairPrice>($"{Route}/{exchange}/{pair}/price");

    public Task<MostRecentTrades> TradesAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MostRecentTrades>($"{Route}/{exchange}/{pair}/trades");

    public Task<MostRecentTrades> TradesAsync(string exchange, string pair, uint since) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MostRecentTrades>($"{Route}/{exchange}/{pair}/trades?since={since}");

    public Task<MostRecentTrades> TradesAsync(string exchange, string pair, int since, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<MostRecentTrades>($"{Route}/{exchange}/{pair}/trades?since={since}&limit={limit}");

    public Task<Summary> SummaryAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<Summary>($"{Route}/{exchange}/{pair}/summary");

    public Task<Summaries> SummariesAsync() =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<Summaries>($"{Route}/summaries");

    public Task<Summaries> SummariesAsync(uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<Summaries>($"{Route}/summaries?limit={limit}");

    public Task<Summaries> SummariesAsync(string cursor, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<Summaries>($"{Route}/summaries?cursor={cursor}&limit={limit}");

    public Task<Summaries> SummariesAsync(string keyBy) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<Summaries>($"{Route}/summaries?keyBy={keyBy}");

    public Task<Summaries> SummariesAsync(string keyBy, string cursor) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<Summaries>($"{Route}/summaries?keyBy={keyBy}&cursor={cursor}");

    public Task<Summaries> SummariesAsync(string keyBy, string cursor, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<Summaries>($"{Route}/summaries?keyBy={keyBy}&cursor={cursor}&limit={limit}");

    public Task<OrderBook> OrderBookAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<OrderBook>($"{Route}/{exchange}/{pair}/orderbook");

    public Task<OrderBook> OrderBookAsync(string exchange, string pair, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<OrderBook>($"{Route}/{exchange}/{pair}/orderbook?limit={limit}");

    public Task<OrderBook> OrderBookAsync(string exchange, string pair, double depth) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<OrderBook>($"{Route}/{exchange}/{pair}/orderbook?depth={depth}");

    public Task<OrderBook> OrderBookAsync(string exchange, string pair, double depth, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<OrderBook>($"{Route}/{exchange}/{pair}/orderbook?depth={depth}&limit={limit}");

    public Task<OrderBook> OrderBookAsync(string exchange, string pair, decimal span) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<OrderBook>($"{Route}/{exchange}/{pair}/orderbook?span={span}");

    public Task<OrderBook> OrderBookAsync(string exchange, string pair, decimal span, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<OrderBook>($"{Route}/{exchange}/{pair}/orderbook?span={span}&limit={limit}");

    public Task<OrderBook> OrderBookAsync(string exchange, string pair, double depth, decimal span) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<OrderBook>($"{Route}/{exchange}/{pair}/orderbook?depth={depth}&span={span}");

    public Task<OrderBook> OrderBookAsync(
        string exchange,
        string pair,
        double depth,
        decimal span,
        uint limit
    ) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<
                OrderBook>($"{Route}/{exchange}/{pair}/orderbook?depth={depth}&span={span}&limit={limit}");

    public Task<OrderBookLiquidity> OrderBookLiquidityAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<OrderBookLiquidity>($"{Route}/{exchange}/{pair}/orderbook/liquidity");

    public Task<OrderBookCalculator> OrderBookCalculatorAsync(string exchange, string pair, double amount) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<OrderBookCalculator>($"{Route}/{exchange}/{pair}/orderbook/calculator?amount={amount}");

    public Task<CandlestickHistories> OHLCCandlesticksAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<CandlestickHistories>($"{Route}/{exchange}/{pair}/ohlc");

    public Task<CandlestickHistories> OHLCCandlesticksAsync(string exchange, string pair, long after) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<CandlestickHistories>($"{Route}/{exchange}/{pair}/ohlc?after={after}");

    public Task<CandlestickHistories> OHLCCandlesticksAsync(string exchange, string pair, DateTimeOffset after) => 
        OHLCCandlesticksAsync(exchange, pair, after.ToUnixTimeSeconds());

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        params TimeFrame[] periods
    ) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<CandlestickHistories>($"{Route}/{exchange}/{pair}/ohlc?periods={ConcatPeriodArray(periods)}");

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        long after,
        params TimeFrame[] periods
    ) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<CandlestickHistories>($"{Route}/{exchange}/{pair}/ohlc?after={after}&periods={ConcatPeriodArray(periods)}");

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        DateTimeOffset after,
        params TimeFrame[] periods
    ) =>
        OHLCCandlesticksAsync(exchange, pair, after.ToUnixTimeSeconds(), periods);

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        long before,
        long after,
        params TimeFrame[] periods
    ) =>
        _httpClientFactory.CreateClient()
            .GetFromJsonAsync<CandlestickHistories>(
                $"{Route}/{exchange}/{pair}/ohlc?before={before}&after={after}&periods={ConcatPeriodArray(periods)}"
            );

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        DateTimeOffset before,
        DateTimeOffset after,
        params TimeFrame[] periods
    ) =>
        OHLCCandlesticksAsync(exchange, pair, before.ToUnixTimeSeconds(), after.ToUnixTimeSeconds(), periods);

    private static StringBuilder ConcatPeriodArray(IEnumerable<TimeFrame> periods) =>
        new StringBuilder(50).AppendJoin(',', periods.Select(x => (int) x));
}
