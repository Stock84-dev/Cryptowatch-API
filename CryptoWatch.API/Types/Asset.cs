using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Asset
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("symbol")] public string Symbol { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("fiat")] public bool Fiat { get; set; }
    [JsonPropertyName("markets")] public Markets1 Markets { get; set; }
}