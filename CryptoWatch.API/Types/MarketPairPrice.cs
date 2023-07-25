using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct MarketPairPrice
{
    [JsonPropertyName("result")] public PriceResult Result { get; set; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; set; }

    public struct PriceResult
    {
        [JsonPropertyName("price")] public decimal Price { get; set; }
    }
}