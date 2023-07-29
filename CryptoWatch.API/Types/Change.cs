using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Change
{
    [JsonConstructor]
    public Change(double percentage, double absolute)
    {
        Percentage = percentage;
        Absolute = absolute;
    }

    [JsonPropertyName("percentage")] public double Percentage { get; }
    [JsonPropertyName("absolute")] public double Absolute { get; }
}