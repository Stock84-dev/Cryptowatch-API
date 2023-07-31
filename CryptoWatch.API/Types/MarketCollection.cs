using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct MarketCollection
{
    [JsonConstructor]
    public MarketCollection(MarketDetails[] result, Cursor cursor, Allowance allowance)
    {
        Result = result;
        Cursor = cursor;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public MarketDetails[] Result { get; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }
}
