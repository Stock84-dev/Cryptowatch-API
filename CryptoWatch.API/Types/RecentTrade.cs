using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct RecentTrade
{
    public RecentTrade(IReadOnlyList<decimal> recentTrades)
    {
        Id = (int)recentTrades[0];
        Timestamp = (long)recentTrades[1];
        Price = (double)recentTrades[2];
        Amount = (double)recentTrades[3];
    }

    [JsonIgnore] public int Id { get; set; }
    [JsonIgnore] public long Timestamp { get; set; }
    [JsonIgnore] public double Price { get; set; }
    [JsonIgnore] public double Amount { get; set; }
}
