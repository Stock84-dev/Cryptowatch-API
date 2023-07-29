using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct Quote
{
    [JsonConstructor]
    public Quote(int id, string route, string symbol, string name, bool fiat)
    {
        Id = id;
        Route = route;
        Symbol = symbol;
        Name = name;
        Fiat = fiat;
    }

    [JsonPropertyName("id")] public int Id { get; }
    [JsonPropertyName("route")] public string Route { get; }
    [JsonPropertyName("symbol")] public string Symbol { get; }
    [JsonPropertyName("name")] public string Name { get; }
    [JsonPropertyName("fiat")] public bool Fiat { get; }
}