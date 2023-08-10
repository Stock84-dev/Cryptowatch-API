using System.Text.Json.Serialization;

namespace CryptoWatch.REST.API.Types;

public readonly struct OrderBookLiquidity
{
    [JsonConstructor]
    public OrderBookLiquidity(ResultDetails result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultDetails Result { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct ResultDetails
    {
        [JsonConstructor]
        public ResultDetails(
            Dictionary<string, Dictionary<int, double>> asks,
            Dictionary<string, Dictionary<int, double>> bids
        )
        {
            Asks = asks;
            Bids = bids;
        }

        [JsonPropertyName("ask")] public Dictionary<string, Dictionary<int, double>> Asks { get; }
        [JsonPropertyName("bid")] public Dictionary<string, Dictionary<int, double>> Bids { get; }
    }
}
