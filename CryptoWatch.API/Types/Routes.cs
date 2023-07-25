using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Routes
{
    [JsonPropertyName("price")] public string Price { get; set; }
    [JsonPropertyName("summary")] public string Summary { get; set; }
    [JsonPropertyName("orderbook")] public string Orderbook { get; set; }
    [JsonPropertyName("trades")] public string Trades { get; set; }
    [JsonPropertyName("ohlc")] public string Ohlc { get; set; }
}