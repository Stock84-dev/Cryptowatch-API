using System.Text.Json.Serialization;

namespace CryptoWatch.REST.API.Types;

public readonly struct Summary
{
    [JsonConstructor]
    public Summary(ResultDetail result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultDetail Result { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public struct ResultDetail
    {
        [JsonConstructor]
        public ResultDetail(Price price, double volume, double volumeQuote)
        {
            Price = price;
            Volume = volume;
            VolumeQuote = volumeQuote;
        }

        [JsonPropertyName("price")] public Price Price { get; }
        [JsonPropertyName("volume")] public double Volume { get; }
        [JsonPropertyName("volumeQuote")] public double VolumeQuote { get; }
    }
}
