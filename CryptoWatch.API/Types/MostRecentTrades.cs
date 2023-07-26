using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct MostRecentTrades
{
    [JsonInclude] [JsonPropertyName("result")]
    public List<List<decimal>> Result;

    [JsonConstructor]
    public MostRecentTrades(List<List<decimal>> result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("allowance")] public Allowance Allowance { get; }
    [JsonIgnore] public List<RecentTrade> RecentTrades => new(Result.Select(x => new RecentTrade(x)));
    [JsonIgnore] public RecentTrade this[int index] => new(Result[index]);
    [JsonIgnore] public int Count => Result.Count;
}
