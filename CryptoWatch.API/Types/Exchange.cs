using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Exchange
{
    [JsonPropertyName("result")] public ResultDetail Result { get; set; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; set; }
    
    public struct ResultDetail
    {
        [JsonPropertyName("id")] public uint Id { get; set; }
        [JsonPropertyName("symbol")] public string Symbol { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("active")] public bool Active { get; set; }
        [JsonPropertyName("routes")] public Route Routes { get; set; }    
    }
}