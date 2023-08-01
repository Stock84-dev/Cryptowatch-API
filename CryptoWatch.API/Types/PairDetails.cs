using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct PairDetails
{
    [JsonConstructor]
    public PairDetails(ResultDetails result, Allowance allowance)
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
            int id,
            string symbol,
            Asset basePair,
            Asset quotePair,
            string route,
            MarketDetails[] markets
        )
        {
            Id = id;
            Symbol = symbol;
            BasePair = basePair;
            QuotePair = quotePair;
            Route = route;
            Markets = markets;
        }

        [JsonPropertyName("id")] public int Id { get; }
        [JsonPropertyName("symbol")] public string Symbol { get; }
        [JsonPropertyName("base")] public Asset BasePair { get; }
        [JsonPropertyName("quote")] public Asset QuotePair { get; }
        public string Route { get; }
        public MarketDetails[] Markets { get; }
    }
}
