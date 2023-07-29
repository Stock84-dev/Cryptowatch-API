using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct Summaries
{
    [JsonConstructor]
    public Summaries(Dictionary<string, ResultDetail> result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public Dictionary<string, ResultDetail> Result { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct ResultDetail
    {
        [JsonConstructor]
        public ResultDetail(Price price, double volume, double volumeBase, double volumeQuote, double volumeUsd)
        {
            Price = price;
            Volume = volume;
            VolumeBase = volumeBase;
            VolumeQuote = volumeQuote;
            VolumeUsd = volumeUsd;
        }

        public Price Price { get; }
        public double Volume { get; }
        public double VolumeBase { get; }
        public double VolumeQuote { get; }
        public double VolumeUsd { get; }
    }
}
