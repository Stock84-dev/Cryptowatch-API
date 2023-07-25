using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct MarketPairDetail
{
    [JsonPropertyName("result")] public Details Result { get; set; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; set; }

    public struct Details
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("exchange")] public string Exchange { get; set; }
        [JsonPropertyName("pair")] public string Pair { get; set; }
        [JsonPropertyName("active")] public bool Active { get; set; }
        [JsonPropertyName("routes")] public Routes Routes { get; set; }
    }
}