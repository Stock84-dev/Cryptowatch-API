using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct PairDetails
{
    [JsonPropertyName("result")] public ResultCollection Result { get; set; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; set; }

    public struct ResultCollection
    {
        [JsonPropertyName("id")] public int id { get; set; }
        [JsonPropertyName("symbol")] public string symbol { get; set; }
        [JsonPropertyName("base")] public Base basePair { get; set; }
        [JsonPropertyName("quote")] public Quote quotePair { get; set; }
        public string route { get; set; }
        public List<MarketDetails> markets { get; set; }
    }
}