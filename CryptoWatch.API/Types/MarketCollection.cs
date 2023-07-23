using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct MarketCollection
{
    [JsonPropertyName("result")] public List<MarketDetails> Result { get; set; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; set; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; set; }
}