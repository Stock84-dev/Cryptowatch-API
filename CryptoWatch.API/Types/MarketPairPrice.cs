using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct MarketPairPrice
{
    [JsonConstructor]
    public MarketPairPrice(PriceResult result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public PriceResult Result { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct PriceResult
    {
        [JsonConstructor]
        public PriceResult(decimal price) => Price = price;

        [JsonPropertyName("price")] public decimal Price { get; }
    }
}
