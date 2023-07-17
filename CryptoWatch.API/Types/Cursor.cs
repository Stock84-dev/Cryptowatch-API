using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Cursor
{
    [JsonPropertyName("last")] public string Last { get; set; }
    [JsonPropertyName("hasMore")] public bool HasMore { get; set; }
}