using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct OrderBook
{
    [JsonConstructor]
    public OrderBook(ResultDetail result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultDetail Result { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct ResultDetail
    {
        [JsonConstructor]
        public ResultDetail(double[][] asks, double[][] bids, long sequenceNumber)
        {
            Asks = asks;
            Bids = bids;
            SequenceNumber = sequenceNumber;
        }

        [JsonPropertyName("asks")] public double[][] Asks { get; }
        [JsonPropertyName("bids")] public double[][] Bids { get; }
        [JsonPropertyName("seqNum")] public long SequenceNumber { get; }
    }
}
