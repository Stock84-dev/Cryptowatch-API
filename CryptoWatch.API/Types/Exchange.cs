using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct Exchange
{
    [JsonConstructor]
    public Exchange(ResultDetail result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultDetail Result { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct ResultDetail
    {
        [JsonConstructor]
        public ResultDetail(uint id, string symbol, string name, bool active, Route routes)
        {
            Id = id;
            Symbol = symbol;
            Name = name;
            Active = active;
            Routes = routes;
        }

        [JsonPropertyName("id")] public uint Id { get; }
        [JsonPropertyName("symbol")] public string Symbol { get; }
        [JsonPropertyName("name")] public string Name { get; }
        [JsonPropertyName("active")] public bool Active { get; }
        [JsonPropertyName("routes")] public Route Routes { get; }
    }
}
