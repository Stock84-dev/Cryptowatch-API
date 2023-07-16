using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Pair
{
    public string symbol { get; set; }
    public int id { get; set; }
    [JsonPropertyName("base")] public Base basePair { get; set; }
    [JsonPropertyName("quote")] public Quote quotePair { get; set; }
    public string route { get; set; }
    public MarketCollection[] markets { get; set; }
}