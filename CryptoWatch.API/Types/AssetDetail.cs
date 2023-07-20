using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct AssetDetail
{
    [JsonPropertyName("result")] public ResultCollection Result { get; set; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; set; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; set; }
    
    public struct ResultCollection
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("sid")] public string SymbolId { get; set; }
        [JsonPropertyName("symbol")] public string Symbol { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("fiat")] public bool Fiat { get; set; }
        [JsonPropertyName("markets")] public Markets1 AssetMarkets { get; set; }
    }
    
    public struct Markets1
    {
        [JsonPropertyName("base")] public List<Bases> BaseMarket { get; set; }
        [JsonPropertyName("quote")] public List<Bases> QuoteMarket { get; set; }
    }
    
    public struct Bases
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("exchange")] public string Exchange { get; set; }
        [JsonPropertyName("pair")] public string Pair { get; set; }
        [JsonPropertyName("active")] public bool Active { get; set; }
        [JsonPropertyName("route")] public string Route { get; set; }
    }
}