using System.Net.Http.Json;
using System.Text;
using CryptoWatch.REST.API.Types;

namespace CryptoWatch.REST.API.Paths;

public readonly struct MarketsApi
{
    private const string Route = "/markets";
    private readonly HttpClient _httpClient;

    internal MarketsApi(HttpClient httpClient) => _httpClient = httpClient;

    public Task<HttpResponseMessage> ExchangeAsync(string exchange, CancellationToken cancellationToken = default) =>
        _httpClient.GetAsync($"{Route}/{exchange}", cancellationToken);

    public Task<MarketCollection> ListAsync(CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<MarketCollection>(Route, cancellationToken);

    public Task<MarketCollection> ListAsync(string cursor, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<MarketCollection>($"{Route}?cursor={cursor}", cancellationToken);

    public Task<MarketCollection> ListAsync(uint limit, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<MarketCollection>($"{Route}?limit={limit}", cancellationToken);

    public Task<MarketCollection> ListAsync(string cursor, uint limit, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<MarketCollection>($"{Route}?cursor={cursor}&limit={limit}", cancellationToken);

    public Task<MarketPairDetail> DetailsAsync(
        string exchange,
        string pair,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<MarketPairDetail>($"{Route}/{exchange}/{pair}", cancellationToken);

    public Task<MarketPrices> PriceAsync(CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<MarketPrices>($"{Route}/prices", cancellationToken);

    public Task<MarketPrices> PriceAsync(string cursor, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<MarketPrices>($"{Route}/prices?cursor={cursor}", cancellationToken);

    public Task<MarketPrices> PriceAsync(string cursor, uint limit, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<MarketPrices>($"{Route}/prices?cursor={cursor}&limit={limit}", cancellationToken);

    public Task<MarketPairPrice> PriceAsync(
        string exchange,
        string pair,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<MarketPairPrice>($"{Route}/{exchange}/{pair}/price", cancellationToken);

    public Task<MostRecentTrades> TradesAsync(
        string exchange,
        string pair,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<MostRecentTrades>($"{Route}/{exchange}/{pair}/trades", cancellationToken);

    public Task<MostRecentTrades> TradesAsync(
        string exchange,
        string pair,
        uint since,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<MostRecentTrades>(
            $"{Route}/{exchange}/{pair}/trades?since={since}",
            cancellationToken
        );

    public Task<MostRecentTrades> TradesAsync(
        string exchange,
        string pair,
        int since,
        uint limit,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<MostRecentTrades>(
            $"{Route}/{exchange}/{pair}/trades?since={since}&limit={limit}",
            cancellationToken
        );

    public Task<Summary> SummaryAsync(string exchange, string pair, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Summary>($"{Route}/{exchange}/{pair}/summary", cancellationToken);

    public Task<Summaries> SummariesAsync(CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Summaries>($"{Route}/summaries", cancellationToken);

    public Task<Summaries> SummariesAsync(uint limit, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Summaries>($"{Route}/summaries?limit={limit}", cancellationToken);

    public Task<Summaries> SummariesAsync(string cursor, uint limit, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Summaries>($"{Route}/summaries?cursor={cursor}&limit={limit}", cancellationToken);

    public Task<Summaries> SummariesAsync(string keyBy, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Summaries>($"{Route}/summaries?keyBy={keyBy}", cancellationToken);

    public Task<Summaries> SummariesAsync(string keyBy, string cursor, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<Summaries>($"{Route}/summaries?keyBy={keyBy}&cursor={cursor}", cancellationToken);

    public Task<Summaries> SummariesAsync(
        string keyBy,
        string cursor,
        uint limit,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<Summaries>(
            $"{Route}/summaries?keyBy={keyBy}&cursor={cursor}&limit={limit}",
            cancellationToken
        );

    public Task<OrderBook> OrderBookAsync(
        string exchange,
        string pair,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<OrderBook>($"{Route}/{exchange}/{pair}/orderbook", cancellationToken);

    public Task<OrderBook> OrderBookAsync(
        string exchange,
        string pair,
        uint limit,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<OrderBook>(
            $"{Route}/{exchange}/{pair}/orderbook?limit={limit}",
            cancellationToken
        );

    public Task<OrderBook> OrderBookAsync(
        string exchange,
        string pair,
        double depth,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<OrderBook>(
            $"{Route}/{exchange}/{pair}/orderbook?depth={depth}",
            cancellationToken
        );

    public Task<OrderBook> OrderBookAsync(
        string exchange,
        string pair,
        double depth,
        uint limit,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<OrderBook>(
            $"{Route}/{exchange}/{pair}/orderbook?depth={depth}&limit={limit}",
            cancellationToken
        );

    public Task<OrderBook> OrderBookAsync(
        string exchange,
        string pair,
        decimal span,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<OrderBook>($"{Route}/{exchange}/{pair}/orderbook?span={span}", cancellationToken);

    public Task<OrderBook> OrderBookAsync(
        string exchange,
        string pair,
        decimal span,
        uint limit,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<OrderBook>(
            $"{Route}/{exchange}/{pair}/orderbook?span={span}&limit={limit}",
            cancellationToken
        );

    public Task<OrderBook> OrderBookAsync(
        string exchange,
        string pair,
        double depth,
        decimal span,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<OrderBook>(
            $"{Route}/{exchange}/{pair}/orderbook?depth={depth}&span={span}",
            cancellationToken
        );

    public Task<OrderBook> OrderBookAsync(
        string exchange,
        string pair,
        double depth,
        decimal span,
        uint limit,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<OrderBook>(
            $"{Route}/{exchange}/{pair}/orderbook?depth={depth}&span={span}&limit={limit}",
            cancellationToken
        );

    public Task<OrderBookLiquidity> OrderBookLiquidityAsync(
        string exchange,
        string pair,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<OrderBookLiquidity>(
            $"{Route}/{exchange}/{pair}/orderbook/liquidity",
            cancellationToken
        );

    public Task<OrderBookCalculator> OrderBookCalculatorAsync(
        string exchange,
        string pair,
        double amount,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<OrderBookCalculator>(
            $"{Route}/{exchange}/{pair}/orderbook/calculator?amount={amount}",
            cancellationToken
        );

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<CandlestickHistories>($"{Route}/{exchange}/{pair}/ohlc", cancellationToken);

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        long after,
        CancellationToken cancellationToken = default
    ) =>
        _httpClient.GetFromJsonAsync<CandlestickHistories>(
            $"{Route}/{exchange}/{pair}/ohlc?after={after}",
            cancellationToken
        );

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        DateTimeOffset after,
        CancellationToken cancellationToken = default
    ) =>
        OHLCCandlesticksAsync(exchange, pair, after.ToUnixTimeSeconds(), cancellationToken);

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        params TimeFrame[] periods
    ) =>
        OHLCCandlesticksAsync(exchange, pair, CancellationToken.None, periods);

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        long after,
        params TimeFrame[] periods
    ) =>
        OHLCCandlesticksAsync(exchange, pair, after, CancellationToken.None, periods);

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        DateTimeOffset after,
        params TimeFrame[] periods
    ) =>
        OHLCCandlesticksAsync(exchange, pair, after, CancellationToken.None, periods);

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        long before,
        long after,
        params TimeFrame[] periods
    ) =>
        OHLCCandlesticksAsync(exchange, pair, before, after, CancellationToken.None, periods);

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        DateTimeOffset before,
        DateTimeOffset after,
        params TimeFrame[] periods
    ) =>
        OHLCCandlesticksAsync(exchange, pair, before, after, CancellationToken.None, periods);

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        CancellationToken cancellationToken = default,
        params TimeFrame[] periods
    ) =>
        _httpClient.GetFromJsonAsync<CandlestickHistories>(
            $"{Route}/{exchange}/{pair}/ohlc?periods={ConcatPeriodArray(periods)}",
            cancellationToken
        );

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        long after,
        CancellationToken cancellationToken = default,
        params TimeFrame[] periods
    ) =>
        _httpClient.GetFromJsonAsync<CandlestickHistories>(
            $"{Route}/{exchange}/{pair}/ohlc?after={after}&periods={ConcatPeriodArray(periods)}",
            cancellationToken
        );

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        DateTimeOffset after,
        CancellationToken cancellationToken = default,
        params TimeFrame[] periods
    ) =>
        OHLCCandlesticksAsync(exchange, pair, after.ToUnixTimeSeconds(), cancellationToken, periods);

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        long before,
        long after,
        CancellationToken cancellationToken = default,
        params TimeFrame[] periods
    ) =>
        _httpClient.GetFromJsonAsync<CandlestickHistories>(
            $"{Route}/{exchange}/{pair}/ohlc?before={before}&after={after}&periods={ConcatPeriodArray(periods)}",
            cancellationToken
        );

    public Task<CandlestickHistories> OHLCCandlesticksAsync(
        string exchange,
        string pair,
        DateTimeOffset before,
        DateTimeOffset after,
        CancellationToken cancellationToken = default,
        params TimeFrame[] periods
    ) =>
        OHLCCandlesticksAsync(
            exchange,
            pair,
            before.ToUnixTimeSeconds(),
            after.ToUnixTimeSeconds(),
            cancellationToken,
            periods
        );

    private static StringBuilder ConcatPeriodArray(IEnumerable<TimeFrame> periods) =>
        new StringBuilder(50).AppendJoin(',', periods.Select(x => (int)x));
}
