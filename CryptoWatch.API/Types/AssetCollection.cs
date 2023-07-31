using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct AssetCollection
{
    [JsonConstructor]
    public AssetCollection(ResultDetails[] result, Cursor cursor, Allowance allowance)
    {
        Result = result;
        Cursor = cursor;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultDetails[] Result { get; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct ResultDetails
    {
        [JsonConstructor]
        public ResultDetails(int id, string symbolId, string symbol, string name, bool fiat, string route)
        {
            Id = id;
            SymbolId = symbolId;
            Symbol = symbol;
            Name = name;
            Fiat = fiat;
            Route = route;
        }

        [JsonPropertyName("id")] public int Id { get; }
        [JsonPropertyName("sid")] public string SymbolId { get; }
        [JsonPropertyName("symbol")] public string Symbol { get; }
        [JsonPropertyName("name")] public string Name { get; }
        [JsonPropertyName("fiat")] public bool Fiat { get; }
        [JsonPropertyName("route")] public string Route { get; }
    }
}
