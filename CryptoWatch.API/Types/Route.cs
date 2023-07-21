using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Route
{
    [JsonPropertyName("markets")] public string Markets { get; set; }
}