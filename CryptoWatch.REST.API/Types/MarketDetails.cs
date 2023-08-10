using System.Text.Json.Serialization;

namespace CryptoWatch.REST.API.Types;

public struct MarketDetails
{
    [JsonConstructor]
    public MarketDetails(int id, string exchange, string pair, bool active, string route)
    {
        Id = id;
        Exchange = exchange;
        Pair = pair;
        Active = active;
        Route = route;
    }

    [JsonPropertyName("id")] public int Id { get; }
    [JsonPropertyName("exchange")] public string Exchange { get; }
    [JsonPropertyName("pair")] public string Pair { get; }
    [JsonPropertyName("active")] public bool Active { get; }
    [JsonPropertyName("route")] public string Route { get; }
}
