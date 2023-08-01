using CryptoWatch.API.Types;
using FluentAssertions;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public sealed class TypesConsistencyTests
{
    [Fact]
    public void Asserts_Allowance_TypeConsistency() =>
        typeof(Allowance).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(decimal), typeof(decimal), typeof(ulong), typeof(string) })
            .And.HaveProperty<decimal>(nameof(Allowance.Cost))
            .And.HaveProperty<decimal>(nameof(Allowance.Remaining))
            .And.HaveProperty<ulong>(nameof(Allowance.RemainingPaid))
            .And.HaveProperty<string>(nameof(Allowance.Upgrade));

    [Fact]
    public void Asserts_Base_TypeConsistency() =>
        typeof(Asset).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
                { typeof(int), typeof(string), typeof(string), typeof(string), typeof(bool), typeof(string) })
            .And.HaveProperty<int>(nameof(Asset.Id))
            .And.HaveProperty<string>(nameof(Asset.Route))
            .And.HaveProperty<string>(nameof(Asset.Symbol))
            .And.HaveProperty<string>(nameof(Asset.Name))
            .And.HaveProperty<bool>(nameof(Asset.Fiat));

    [Fact]
    public void Asserts_AssetCollection_TypeConsistency() =>
        typeof(AssetCollection).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(Asset[]), typeof(Cursor), typeof(Allowance) })
            .And.HaveProperty<Asset[]>(nameof(AssetCollection.Result))
            .And.HaveProperty<Cursor>(nameof(AssetCollection.Cursor))
            .And.HaveProperty<Allowance>(nameof(AssetCollection.Allowance));

    [Fact]
    public void Asserts_AssetDetail_TypeConsistency()
    {
        typeof(AssetDetail).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(AssetDetail.ResultDetails), typeof(Cursor), typeof(Allowance) })
            .And.HaveProperty<AssetDetail.ResultDetails>(nameof(AssetDetail.Result))
            .And.HaveProperty<Cursor>(nameof(AssetDetail.Cursor))
            .And.HaveProperty<Allowance>(nameof(AssetDetail.Allowance));
        typeof(AssetDetail.ResultDetails).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
            {
                typeof(int), typeof(string), typeof(string), typeof(string), typeof(bool),
                typeof(AssetDetail.Markets)
            })
            .And.HaveProperty<int>(nameof(AssetDetail.ResultDetails.Id))
            .And.HaveProperty<string>(nameof(AssetDetail.ResultDetails.SymbolId))
            .And.HaveProperty<string>(nameof(AssetDetail.ResultDetails.Symbol))
            .And.HaveProperty<string>(nameof(AssetDetail.ResultDetails.Name))
            .And.HaveProperty<bool>(nameof(AssetDetail.ResultDetails.Fiat))
            .And.HaveProperty<AssetDetail.Markets>(nameof(AssetDetail.ResultDetails.AssetMarkets));
        typeof(AssetDetail.Markets).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(AssetDetail.Base[]), typeof(AssetDetail.Base[]) })
            .And.HaveProperty<AssetDetail.Base[]>(nameof(AssetDetail.Markets.BaseMarket))
            .And.HaveProperty<AssetDetail.Base[]>(nameof(AssetDetail.Markets.QuoteMarket));
        typeof(AssetDetail.Base).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
                { typeof(int), typeof(string), typeof(string), typeof(bool), typeof(string) })
            .And.HaveProperty<int>(nameof(AssetDetail.Base.Id))
            .And.HaveProperty<string>(nameof(AssetDetail.Base.Exchange))
            .And.HaveProperty<string>(nameof(AssetDetail.Base.Pair))
            .And.HaveProperty<bool>(nameof(AssetDetail.Base.Active))
            .And.HaveProperty<string>(nameof(AssetDetail.Base.Route));
    }

    [Fact]
    public void Asserts_Cursor_TypeConsistency() =>
        typeof(Cursor).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(string), typeof(bool) })
            .And.HaveProperty<string>(nameof(Cursor.Last))
            .And.HaveProperty<bool>(nameof(Cursor.HasMore));

    [Fact]
    public void Asserts_Exchange_TypeConsistency()
    {
        typeof(Exchange).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(Exchange.ResultDetail), typeof(Allowance) })
            .And.HaveProperty<Exchange.ResultDetail>(nameof(Exchange.Result))
            .And.HaveProperty<Allowance>(nameof(Exchange.Allowance));
        typeof(Exchange.ResultDetail).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
            {
                typeof(uint), typeof(string), typeof(string), typeof(bool),
                typeof(Route)
            })
            .And.HaveProperty<uint>(nameof(Exchange.ResultDetail.Id))
            .And.HaveProperty<string>(nameof(Exchange.ResultDetail.Symbol))
            .And.HaveProperty<string>(nameof(Exchange.ResultDetail.Name))
            .And.HaveProperty<bool>(nameof(Exchange.ResultDetail.Active))
            .And.HaveProperty<Route>(nameof(Exchange.ResultDetail.Routes));
    }

    [Fact]
    public void Asserts_Exchanges_TypeConsistency()
    {
        typeof(Exchanges).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(Exchanges.ResultDetails[]), typeof(Cursor), typeof(Allowance) })
            .And.HaveProperty<Exchanges.ResultDetails[]>(nameof(Exchanges.Result))
            .And.HaveProperty<Cursor>(nameof(Exchanges.Cursor))
            .And.HaveProperty<Allowance>(nameof(Exchanges.Allowance));
        typeof(Exchanges.ResultDetails).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(uint), typeof(string), typeof(string), typeof(string), typeof(bool) })
            .And.HaveProperty<uint>(nameof(Exchanges.ResultDetails.Id))
            .And.HaveProperty<string>(nameof(Exchanges.ResultDetails.Symbol))
            .And.HaveProperty<string>(nameof(Exchanges.ResultDetails.Name))
            .And.HaveProperty<string>(nameof(Exchanges.ResultDetails.Route))
            .And.HaveProperty<bool>(nameof(Exchanges.ResultDetails.Active));
    }

    [Fact]
    public void Asserts_MarketCollection_TypeConsistency() =>
        typeof(MarketCollection).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(MarketDetails[]), typeof(Cursor), typeof(Allowance) })
            .And.HaveProperty<MarketDetails[]>(nameof(MarketCollection.Result))
            .And.HaveProperty<Cursor>(nameof(MarketCollection.Cursor))
            .And.HaveProperty<Allowance>(nameof(MarketCollection.Allowance));

    [Fact]
    public void Asserts_MarketDetails_TypeConsistency() =>
        typeof(MarketDetails).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(int), typeof(string), typeof(string), typeof(bool), typeof(string) })
            .And.HaveProperty<int>(nameof(MarketDetails.Id))
            .And.HaveProperty<string>(nameof(MarketDetails.Exchange))
            .And.HaveProperty<string>(nameof(MarketDetails.Pair))
            .And.HaveProperty<bool>(nameof(MarketDetails.Active))
            .And.HaveProperty<string>(nameof(MarketDetails.Route));

    [Fact]
    public void Asserts_MarketPairDetail_TypeConsistency()
    {
        typeof(MarketPairDetail).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(MarketPairDetail.ResultDetails), typeof(Allowance) })
            .And.HaveProperty<MarketPairDetail.ResultDetails>(nameof(MarketPairDetail.Result))
            .And.HaveProperty<Allowance>(nameof(MarketPairDetail.Allowance));

        typeof(MarketPairDetail.ResultDetails).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
            {
                typeof(int), typeof(string), typeof(string), typeof(bool),
                typeof(Routes)
            })
            .And.HaveProperty<int>(nameof(MarketPairDetail.ResultDetails.Id))
            .And.HaveProperty<string>(nameof(MarketPairDetail.ResultDetails.Exchange))
            .And.HaveProperty<string>(nameof(MarketPairDetail.ResultDetails.Pair))
            .And.HaveProperty<bool>(nameof(MarketPairDetail.ResultDetails.Active))
            .And.HaveProperty<Routes>(nameof(MarketPairDetail.ResultDetails.Routes));
    }

    [Fact]
    public void Asserts_MarketPairPrice_TypeConsistency()
    {
        typeof(MarketPairPrice).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(MarketPairPrice.PriceResult), typeof(Allowance) })
            .And.HaveProperty<MarketPairPrice.PriceResult>(nameof(MarketPairPrice.Result))
            .And.HaveProperty<Allowance>(nameof(MarketPairPrice.Allowance));

        typeof(MarketPairPrice.PriceResult).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(decimal) })
            .And.HaveProperty<decimal>(nameof(MarketPairPrice.PriceResult.Price));
    }

    [Fact]
    public void Asserts_MarketPrices_TypeConsistency() =>
        typeof(MarketPrices).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(Dictionary<string, decimal>), typeof(Cursor), typeof(Allowance) })
            .And.HaveProperty<Dictionary<string, decimal>>(nameof(MarketPrices.Result))
            .And.HaveProperty<Cursor>(nameof(MarketPrices.Cursor))
            .And.HaveProperty<Allowance>(nameof(MarketPrices.Allowance));

    [Fact]
    public void Asserts_MostRecentTrades_TypeConsistency()
    {
        typeof(MostRecentTrades).Should()
            .NotHaveDefaultConstructor();
        typeof(MostRecentTrades).Should()
            .HaveProperty<RecentTrade[]>(nameof(MostRecentTrades.RecentTrades))
            .Which.Should()
            .NotBeWritable();
        typeof(MostRecentTrades).Should()
            .HaveProperty<Allowance>(nameof(MostRecentTrades.Allowance))
            .Which.Should()
            .NotBeWritable();
    }

    [Fact]
    public void Asserts_OrderBook_TypeConsistency()
    {
        typeof(OrderBook).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(OrderBook.ResultDetail), typeof(Allowance) })
            .And.HaveProperty<OrderBook.ResultDetail>(nameof(OrderBook.Result))
            .And.HaveProperty<Allowance>(nameof(OrderBook.Allowance));

        typeof(OrderBook.ResultDetail).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(double[][]), typeof(double[][]), typeof(long) })
            .And.HaveProperty<double[][]>(nameof(OrderBook.ResultDetail.Asks))
            .And.HaveProperty<double[][]>(nameof(OrderBook.ResultDetail.Bids))
            .And.HaveProperty<long>(nameof(OrderBook.ResultDetail.SequenceNumber));
    }

    [Fact]
    public void Asserts_OrderBookCalculator_TypeConsistency()
    {
        typeof(OrderBookCalculator).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(OrderBookCalculator.ResultDetail), typeof(Allowance) })
            .And.HaveProperty<OrderBookCalculator.ResultDetail>(nameof(OrderBookCalculator.Result))
            .And.HaveProperty<Allowance>(nameof(OrderBookCalculator.Allowance));

        typeof(OrderBookCalculator.ResultDetail).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
                { typeof(OrderBookCalculator.BuyTransaction), typeof(OrderBookCalculator.SellTransaction) })
            .And.HaveProperty<OrderBookCalculator.BuyTransaction>(nameof(OrderBookCalculator.ResultDetail.Buy))
            .And.HaveProperty<OrderBookCalculator.SellTransaction>(nameof(OrderBookCalculator.ResultDetail.Sell));

        typeof(OrderBookCalculator.BuyTransaction).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
            {
                typeof(double), typeof(double), typeof(double), typeof(double), typeof(double), typeof(double),
                typeof(double)
            })
            .And.HaveProperty<double>(nameof(OrderBookCalculator.BuyTransaction.AveragePrice))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.BuyTransaction.AverageDelta))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.BuyTransaction.AverageBps))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.BuyTransaction.ReachPrice))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.BuyTransaction.ReachDelta))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.BuyTransaction.ReachDeltaBps))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.BuyTransaction.Spend));

        typeof(OrderBookCalculator.SellTransaction).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
            {
                typeof(double), typeof(double), typeof(double), typeof(double), typeof(double), typeof(double),
                typeof(double)
            })
            .And.HaveProperty<double>(nameof(OrderBookCalculator.SellTransaction.AveragePrice))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.SellTransaction.AverageDelta))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.SellTransaction.AverageBps))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.SellTransaction.ReachPrice))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.SellTransaction.ReachDelta))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.SellTransaction.ReachDeltaBps))
            .And.HaveProperty<double>(nameof(OrderBookCalculator.SellTransaction.Receive));
    }

    [Fact]
    public void Asserts_OrderBookLiquidity_TypeConsistency()
    {
        typeof(OrderBookLiquidity).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(OrderBookLiquidity.ResultDetails), typeof(Allowance) })
            .And.HaveProperty<OrderBookLiquidity.ResultDetails>(nameof(OrderBookLiquidity.Result))
            .And.HaveProperty<Allowance>(nameof(OrderBookLiquidity.Allowance));
        typeof(OrderBookLiquidity.ResultDetails).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
            {
                typeof(Dictionary<string, Dictionary<int, double>>),
                typeof(Dictionary<string, Dictionary<int, double>>)
            })
            .And.HaveProperty<Dictionary<string, Dictionary<int, double>>>(
                nameof(OrderBookLiquidity.ResultDetails.Asks))
            .And.HaveProperty<Dictionary<string, Dictionary<int, double>>>(
                nameof(OrderBookLiquidity.ResultDetails.Bids));
    }

    [Fact]
    public void Asserts_PairDetails_TypeConsistency()
    {
        typeof(PairDetails).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(PairDetails.ResultDetails), typeof(Allowance) })
            .And.HaveProperty<PairDetails.ResultDetails>(nameof(PairDetails.Result))
            .And.HaveProperty<Allowance>(nameof(PairDetails.Allowance));

        typeof(PairDetails.ResultDetails).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
            {
                typeof(int), typeof(string), typeof(Asset), typeof(Asset),
                typeof(string), typeof(MarketDetails[])
            })
            .And.HaveProperty<int>(nameof(PairDetails.ResultDetails.Id))
            .And.HaveProperty<string>(nameof(PairDetails.ResultDetails.Symbol))
            .And.HaveProperty<Asset>(nameof(PairDetails.ResultDetails.BasePair))
            .And.HaveProperty<Asset>(nameof(PairDetails.ResultDetails.QuotePair))
            .And.HaveProperty<string>(nameof(PairDetails.ResultDetails.Route))
            .And.HaveProperty<MarketDetails[]>(nameof(PairDetails.ResultDetails.Markets));
    }

    [Fact]
    public void Asserts_Pairs_TypeConsistency()
    {
        typeof(Pairs).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(Pairs.ResultDetails[]), typeof(Cursor), typeof(Allowance) })
            .And.HaveProperty<Pairs.ResultDetails[]>(nameof(Pairs.Result))
            .And.HaveProperty<Cursor>(nameof(Pairs.Cursor))
            .And.HaveProperty<Allowance>(nameof(Pairs.Allowance));
        typeof(Pairs.ResultDetails).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
                { typeof(int), typeof(string), typeof(Asset), typeof(Asset), typeof(string), typeof(string) })
            .And.HaveProperty<int>(nameof(Pairs.ResultDetails.Id))
            .And.HaveProperty<string>(nameof(Pairs.ResultDetails.Symbol))
            .And.HaveProperty<Asset>(nameof(Pairs.ResultDetails.Base))
            .And.HaveProperty<Asset>(nameof(Pairs.ResultDetails.Quote))
            .And.HaveProperty<string>(nameof(Pairs.ResultDetails.Route))
            .And.HaveProperty<string>(nameof(Pairs.ResultDetails.FuturesContractPeriod));
    }

    [Fact]
    public void Asserts_Price_TypeConsistency() =>
        typeof(Price).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(double), typeof(double), typeof(double), typeof(Change) })
            .And.HaveProperty<double>(nameof(Price.Last))
            .And.HaveProperty<double>(nameof(Price.High))
            .And.HaveProperty<double>(nameof(Price.Low))
            .And.HaveProperty<Change>(nameof(Price.Change));

    [Fact]
    public void Asserts_RecentTrade_TypeConsistency() =>
        typeof(RecentTrade).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(IReadOnlyList<decimal>) })
            .And.HaveProperty<int>(nameof(RecentTrade.Id))
            .And.HaveProperty<long>(nameof(RecentTrade.Timestamp))
            .And.HaveProperty<decimal>(nameof(RecentTrade.Price))
            .And.HaveProperty<decimal>(nameof(RecentTrade.Amount));

    [Fact]
    public void Asserts_Route_TypeConsistency() =>
        typeof(Route).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[] { typeof(string) })
            .And.HaveProperty<string>(nameof(Route.Markets));

    [Fact]
    public void Asserts_Routes_TypeConsistency() =>
        typeof(Routes).Should()
            .NotHaveDefaultConstructor()
            .And.HaveConstructor(new[]
                { typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) })
            .And.HaveProperty<string>(nameof(Routes.Price))
            .And.HaveProperty<string>(nameof(Routes.Summary))
            .And.HaveProperty<string>(nameof(Routes.Orderbook))
            .And.HaveProperty<string>(nameof(Routes.Trades))
            .And.HaveProperty<string>(nameof(Routes.Ohlc));

    [Fact]
    public void Asserts_Summaries_TypeConsistency()
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
        typeof(Summaries).Should()
            .HaveProperty<Allowance>(nameof(Summaries.Allowance))
            .Which.Should()
            .NotBeWritable();
        typeof(Summaries).Should()
            .HaveProperty<Cursor>(nameof(Summaries.Cursor))
            .Which.Should()
            .NotBeWritable();
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
}
