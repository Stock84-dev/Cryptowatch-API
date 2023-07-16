using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Markets1
{
    [JsonPropertyName("base")] public Bases[] baseMarket { get; set; }
    [JsonPropertyName("quote")] public Quotes[] quoteMarket { get; set; }
}