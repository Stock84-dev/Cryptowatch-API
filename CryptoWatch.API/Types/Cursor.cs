using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct Cursor
{
    [JsonConstructor]
    public Cursor(string last, bool hasMore)
    {
        Last = last;
        HasMore = hasMore;
    }

    [JsonPropertyName("last")] public string Last { get; }
    [JsonPropertyName("hasMore")] public bool HasMore { get; }
}
