using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Allowance
{
    [JsonConstructor]
    public Allowance(decimal cost, decimal remaining, ulong remainingPaid, string upgrade)
    {
        Cost = cost;
        Remaining = remaining;
        RemainingPaid = remainingPaid;
        Upgrade = upgrade;
    }

    [JsonPropertyName("cost")] public decimal Cost { get; }
    [JsonPropertyName("remaining")] public decimal Remaining { get; }
    [JsonPropertyName("remainingPaid")] public ulong RemainingPaid { get; }
    [JsonPropertyName("upgrade")] public string Upgrade { get; }
}
