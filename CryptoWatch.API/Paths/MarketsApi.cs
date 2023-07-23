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

    public Task<HttpResponseMessage> ListAsync(string cursor) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}?cursor={cursor}");

    public Task<HttpResponseMessage> ListAsync(uint limit) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}?limit={limit}");

    public Task<HttpResponseMessage> ListAsync(string cursor, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}?cursor={cursor}&limit={limit}");

    public Task<HttpResponseMessage> DetailsAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}");

    public Task<HttpResponseMessage> PriceAsync() =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/prices");

    public Task<HttpResponseMessage> PriceAsync(string cursor) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/prices?cursor={cursor}");

    public Task<HttpResponseMessage> PriceAsync(string cursor, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}?cursor={cursor}&limit={limit}");

    public Task<HttpResponseMessage> PriceAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/price");

    public Task<HttpResponseMessage> TradesAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/trades");

    public Task<HttpResponseMessage> TradesAsync(string exchange, string pair, uint since) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/trades?since={since}");

    public Task<HttpResponseMessage> TradesAsync(string exchange, string pair, int since, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/trades?since={since}&limit={limit}");

    public Task<HttpResponseMessage> SummaryAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/summary");

    public Task<HttpResponseMessage> SummariesAsync() =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/summaries");

    public Task<HttpResponseMessage> SummariesAsync(string keyBy) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/summaries?keyBy={keyBy}");

    public Task<HttpResponseMessage> SummariesAsync(string keyBy, string cursor) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/summaries?keyBy={keyBy}&cursor={cursor}");

    public Task<HttpResponseMessage> SummariesAsync(string keyBy, string cursor, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/summaries?keyBy={keyBy}&cursor={cursor}&limit={limit}");

    public Task<HttpResponseMessage> OrderBookAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook");

    public Task<HttpResponseMessage> OrderBookAsync(string exchange, string pair, double depth) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?depth={depth}");

    public Task<HttpResponseMessage> OrderBookAsync(string exchange, string pair, double depth, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?depth={depth}&limit={limit}");

    public Task<HttpResponseMessage> OrderBookAsync(string exchange, string pair, decimal span) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?span={span}");

    public Task<HttpResponseMessage> OrderBookAsync(string exchange, string pair, decimal span, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?span={span}&limit={limit}");

    public Task<HttpResponseMessage> OrderBookAsync(string exchange, string pair, double depth, decimal span) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?depth={depth}&span={span}");

    public Task<HttpResponseMessage> OrderBookAsync(string exchange, string pair, double depth, decimal span, uint limit) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook?depth={depth}&span={span}&limit={limit}");

    public Task<HttpResponseMessage> OrderBookLiquidityAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook/liquidity");

    public Task<HttpResponseMessage> OrderBookCalculatorAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook/calculator");

    public Task<HttpResponseMessage> OrderBookCalculatorAsync(string exchange, string pair, decimal amount) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/orderbook/calculator?amount={amount}");

    public Task<HttpResponseMessage> OHLCCandlesticksAsync(string exchange, string pair) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/ohlc");

    public Task<HttpResponseMessage> OHLCCandlesticksAsync(string exchange, string pair, long after) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/ohlc?after={after}");

    public Task<HttpResponseMessage> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        long after,
        IEnumerable<TimeFrame> periods
    ) =>
        _httpClientFactory.CreateClient()
            .GetAsync($"{Route}/{exchange}/{pair}/ohlc?after={after}&periods={ConcatPeriodArray(periods)}");

    public Task<HttpResponseMessage> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        long before,
        long after,
        IEnumerable<TimeFrame> periods
    ) =>
        _httpClientFactory.CreateClient()
            .GetAsync(
                $"{Route}/{exchange}/{pair}/ohlc?before={before}&after={after}&periods={ConcatPeriodArray(periods)}"
            );

    private static StringBuilder ConcatPeriodArray(IEnumerable<TimeFrame> periods) =>
        new StringBuilder(capacity: 50).AppendJoin(',', periods);
}