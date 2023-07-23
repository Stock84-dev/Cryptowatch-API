using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct MarketDetails
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("exchange")] public string Exchange { get; set; }
    [JsonPropertyName("pair")] public string Pair { get; set; }
    [JsonPropertyName("active")] public bool Active { get; set; }
    [JsonPropertyName("route")] public string Route { get; set; }
}