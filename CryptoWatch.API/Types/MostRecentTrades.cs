using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct MostRecentTrades
{
    [JsonInclude] [JsonPropertyName("result")]
    public readonly decimal[][] Result;

    [JsonConstructor]
    public MostRecentTrades(decimal[][] result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    [JsonIgnore]
    public RecentTrade[] RecentTrades => Result.Select(x => new RecentTrade(x))
        .ToArray();

    [JsonIgnore] public RecentTrade this[int index] => new(Result[index]);
    [JsonIgnore] public int Count => Result.Length;
}
