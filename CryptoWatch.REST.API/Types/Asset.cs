using System.Text.Json.Serialization;

namespace CryptoWatch.REST.API.Types;

public readonly struct Asset
{
    [JsonConstructor]
    public Asset(int id, string route, string symbol, string name, bool fiat, string symbolId)
    {
        Id = id;
        Route = route;
        Symbol = symbol;
        Name = name;
        Fiat = fiat;
        SymbolId = symbolId;
    }

    [JsonPropertyName("id")] public int Id { get; }
    [JsonPropertyName("route")] public string Route { get; }
    [JsonPropertyName("symbol")] public string Symbol { get; }
    [JsonPropertyName("name")] public string Name { get; }
    [JsonPropertyName("fiat")] public bool Fiat { get; }
    [JsonPropertyName("sid")] public string SymbolId { get; }
}
