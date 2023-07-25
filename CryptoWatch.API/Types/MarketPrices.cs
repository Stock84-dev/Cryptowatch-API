using System.Dynamic;
using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct MarketPrices
{
    [JsonPropertyName("result")] public ExpandoObject Result { get; set; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; set; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; set; }
}