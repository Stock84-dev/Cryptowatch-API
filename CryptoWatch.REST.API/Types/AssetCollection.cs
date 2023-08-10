using System.Text.Json.Serialization;

namespace CryptoWatch.REST.API.Types;

public readonly struct AssetCollection
{
    [JsonConstructor]
    public AssetCollection(Asset[] result, Cursor cursor, Allowance allowance)
    {
        Result = result;
        Cursor = cursor;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public Asset[] Result { get; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }
}
