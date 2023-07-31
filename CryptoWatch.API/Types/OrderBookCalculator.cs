using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct OrderBookCalculator
{
    [JsonConstructor]
    public OrderBookCalculator(ResultDetail result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultDetail Result { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct ResultDetail
    {
        [JsonConstructor]
        public ResultDetail(BuyTransaction buy, SellTransaction sell)
        {
            Buy = buy;
            Sell = sell;
        }

        [JsonPropertyName("buy")] public BuyTransaction Buy { get; }
        [JsonPropertyName("sell")] public SellTransaction Sell { get; }
    }

    public readonly struct BuyTransaction
    {
        [JsonConstructor]
        public BuyTransaction(
            double averagePrice,
            double averageDelta,
            double averageBps,
            double reachPrice,
            double reachDelta,
            double reachDeltaBps,
            double spend
        )
        {
            AveragePrice = averagePrice;
            AverageDelta = averageDelta;
            AverageBps = averageBps;
            ReachPrice = reachPrice;
            ReachDelta = reachDelta;
            ReachDeltaBps = reachDeltaBps;
            Spend = spend;
        }

        [JsonPropertyName("avgPrice")] public double AveragePrice { get; }
        [JsonPropertyName("avgDelta")] public double AverageDelta { get; }
        [JsonPropertyName("avgDeltaBps")] public double AverageBps { get; }
        [JsonPropertyName("reachPrice")] public double ReachPrice { get; }
        [JsonPropertyName("reachDelta")] public double ReachDelta { get; }
        [JsonPropertyName("reachDeltaBps")] public double ReachDeltaBps { get; }
        [JsonPropertyName("spend")] public double Spend { get; }
    }

    public readonly struct SellTransaction
    {
        [JsonConstructor]
        public SellTransaction(
            double averagePrice,
            double averageDelta,
            double averageBps,
            double reachPrice,
            double reachDelta,
            double reachDeltaBps,
            double receive
        )
        {
            AveragePrice = averagePrice;
            AverageDelta = averageDelta;
            AverageBps = averageBps;
            ReachPrice = reachPrice;
            ReachDelta = reachDelta;
            ReachDeltaBps = reachDeltaBps;
            Receive = receive;
        }

        [JsonPropertyName("avgPrice")] public double AveragePrice { get; }
        [JsonPropertyName("avgDelta")] public double AverageDelta { get; }
        [JsonPropertyName("avgDeltaBps")] public double AverageBps { get; }
        [JsonPropertyName("reachPrice")] public double ReachPrice { get; }
        [JsonPropertyName("reachDelta")] public double ReachDelta { get; }
        [JsonPropertyName("reachDeltaBps")] public double ReachDeltaBps { get; }
        [JsonPropertyName("receive")] public double Receive { get; }
    }
}
