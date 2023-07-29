using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct PairDetails
{
    [JsonConstructor]
    public PairDetails(ResultCollection result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultCollection Result { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct ResultCollection
    {
        [JsonConstructor]
        public ResultCollection(
            int id,
            string symbol,
            Base basePair,
            Quote quotePair,
            string route,
            List<MarketDetails> markets
        )
        {
            this.id = id;
            this.symbol = symbol;
            this.basePair = basePair;
            this.quotePair = quotePair;
            this.route = route;
            this.markets = markets;
        }

        [JsonPropertyName("id")] public int id { get; }
        [JsonPropertyName("symbol")] public string symbol { get; }
        [JsonPropertyName("base")] public Base basePair { get; }
        [JsonPropertyName("quote")] public Quote quotePair { get; }
        public string route { get; }
        public List<MarketDetails> markets { get; }
    }
}
