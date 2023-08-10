using System.Text.Json.Serialization;

namespace CryptoWatch.REST.API.Types;

public readonly struct Price
{
    [JsonConstructor]
    public Price(double last, double high, double low, Change change)
    {
        Last = last;
        High = high;
        Low = low;
        Change = change;
    }

    [JsonPropertyName("last")] public double Last { get; }
    [JsonPropertyName("high")] public double High { get; }
    [JsonPropertyName("low")] public double Low { get; }
    [JsonPropertyName("change")] public Change Change { get; }
}
