using System.Text.Json.Serialization;

namespace CryptoWatch.REST.API.Types;

public readonly struct MarketPrices
{
    [JsonConstructor]
    public MarketPrices(Dictionary<string, decimal> result, Cursor cursor, Allowance allowance)
    {
        Result = result;
        Cursor = cursor;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public Dictionary<string, decimal> Result { get; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }
}
