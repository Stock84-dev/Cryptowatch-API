using System.Text.Json.Serialization;

namespace CryptoWatch.REST.API.Types;

public readonly struct Exchanges
{
    [JsonConstructor]
    public Exchanges(ResultDetails[] result, Cursor cursor, Allowance allowance)
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
        public ResultDetails(uint id, string symbol, string name, string route, bool active)
        {
            Id = id;
            Symbol = symbol;
            Name = name;
            Route = route;
            Active = active;
        }

        [JsonPropertyName("id")] public uint Id { get; }
        [JsonPropertyName("symbol")] public string Symbol { get; }
        [JsonPropertyName("name")] public string Name { get; }
        [JsonPropertyName("route")] public string Route { get; }
        [JsonPropertyName("active")] public bool Active { get; }
    }
}
