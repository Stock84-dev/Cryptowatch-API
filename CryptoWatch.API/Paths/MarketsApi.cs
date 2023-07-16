using System.Text;
using CryptoWatch.API.Types;

namespace CryptoWatch.API.Paths;

public readonly struct MarketsApi
{
    private const uint Pagination = 20_000;
    private const string Route = "/markets";
    private readonly ApiConfiguration _apiConfiguration;

    internal MarketsApi(ApiConfiguration apiConfiguration) => _apiConfiguration = apiConfiguration;

    public Task<HttpResponseMessage> Exchange(string exchange) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}");

    public Task<HttpResponseMessage> List() => 
        _apiConfiguration.CreateClient()
            .GetAsync(Route);

    public Task<HttpResponseMessage> List(string cursor) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?cursor={cursor}");

    public Task<HttpResponseMessage> List(uint limit) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?limit={limit}");

    public Task<HttpResponseMessage> List(string cursor, uint limit) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?cursor={cursor}&limit={limit}");

    public Task<HttpResponseMessage> Details(string exchange, string pair) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}");

    public Task<HttpResponseMessage> Price() => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/prices");

    public Task<HttpResponseMessage> Price(string cursor) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/prices?cursor={cursor}");

    public Task<HttpResponseMessage> Price(string cursor, uint limit) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}?cursor={cursor}&limit={limit}");

    public Task<HttpResponseMessage> Price(string exchange, string pair) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/price");

    public Task<HttpResponseMessage> Trades(string exchange, string pair) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/trades");

    public Task<HttpResponseMessage> Trades(string exchange, string pair, uint since) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/trades?since={since}");

    public Task<HttpResponseMessage> Trades(string exchange, string pair, int since, uint limit) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/trades?since={since}&limit={limit}");

    public Task<HttpResponseMessage> Summary(string exchange, string pair) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/summary");

    public Task<HttpResponseMessage> Summaries() => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/summaries");

    public Task<HttpResponseMessage> Summaries(string keyBy) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/summaries?keyBy={keyBy}");

    public Task<HttpResponseMessage> Summaries(string keyBy, string cursor) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/summaries?keyBy={keyBy}&cursor={cursor}");

    public Task<HttpResponseMessage> Summaries(string keyBy, string cursor, uint limit) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/summaries?keyBy={keyBy}&cursor={cursor}&limit={limit}");

    public Task<HttpResponseMessage> OrderBook(string exchange, string pair) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook");

    public Task<HttpResponseMessage> OrderBook(string exchange, string pair, double depth) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?depth={depth}");

    public Task<HttpResponseMessage> OrderBook(string exchange, string pair, double depth, uint limit) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?depth={depth}&limit={limit}");

    public Task<HttpResponseMessage> OrderBook(string exchange, string pair, decimal span) => 
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?span={span}");

    public Task<HttpResponseMessage> OrderBook(string exchange, string pair, decimal span, uint limit) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?span={span}&limit={limit}");

    public Task<HttpResponseMessage> OrderBook(string exchange, string pair, double depth, decimal span) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?depth={depth}&span={span}");

    public Task<HttpResponseMessage> OrderBook(string exchange, string pair, double depth, decimal span, uint limit) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?depth={depth}&span={span}&limit={limit}");

    public Task<HttpResponseMessage> OrderBookLiquidity(string exchange, string pair) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook/liquidity");

    public Task<HttpResponseMessage> OrderBookCalculator(string exchange, string pair) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook/calculator");

    public Task<HttpResponseMessage> OrderBookCalculator(string exchange, string pair, decimal amount) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook/calculator?amount={amount}");

    public Task<HttpResponseMessage> OHLCCandlesticks(string exchange, string pair) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/ohlc");

    public Task<HttpResponseMessage> OHLCCandlesticks(string exchange, string pair, long after) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/ohlc?after={after}");

    public Task<HttpResponseMessage> OHLCCandlesticks(
        string exchange,
        string pair,
        long after,
        IEnumerable<TimeFrame> periods
    ) =>
        _apiConfiguration.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/ohlc?after={after}&periods={ConcatPeriodArray(periods)}");

    public Task<HttpResponseMessage> OHLCCandlesticks(
        string exchange,
        string pair,
        long before,
        long after,
        IEnumerable<TimeFrame> periods
    ) =>
        _apiConfiguration.CreateClient()
            .GetAsync(
                $"{Route}/{exchange}/{pair}/ohlc?before={before}&after={after}&periods={ConcatPeriodArray(periods)}"
            );

    private static StringBuilder ConcatPeriodArray(IEnumerable<TimeFrame> periods) =>
        new StringBuilder(50).AppendJoin(',', periods);
}