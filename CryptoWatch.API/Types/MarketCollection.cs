using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct MarketCollection
{
    [JsonPropertyName("result")]
    public List<Collection> Result { get; set; }
    [JsonPropertyName("cursor")]
    public Cursor Cursor { get; set; }
    [JsonPropertyName("allowance")]
    public Allowance Allowance { get; set; }
    
    public struct Collection
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("exchange")]
        public string Exchange { get; set; }
        [JsonPropertyName("pair")]
        public string Pair { get; set; }
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        [JsonPropertyName("route")]
        public string Route { get; set; }
    }
}