using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Allowance
{
    [JsonPropertyName("cost")] public decimal Cost { get; set; }
    [JsonPropertyName("remaining")] public decimal Remaining { get; set; }
    [JsonPropertyName("upgrade")] public string Upgrade { get; set; }
}