using System.Text.Json.Serialization;

namespace CryptoWatch.REST.API.Types;

public readonly struct RecentTrade
{
    public RecentTrade(IReadOnlyList<decimal> recentTrades)
    {
        Id = (int)recentTrades[0];
        Timestamp = (long)recentTrades[1];
        Price = recentTrades[2];
        Amount = recentTrades[3];
    }

    [JsonIgnore] public int Id { get; }
    [JsonIgnore] public long Timestamp { get; }
    [JsonIgnore] public decimal Price { get; }
    [JsonIgnore] public decimal Amount { get; }
}
