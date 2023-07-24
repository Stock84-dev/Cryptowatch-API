using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct AssetCollection
{
    [JsonPropertyName("result")] public List<ResultCollection> Result { get; set; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; set; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; set; }

    public struct ResultCollection
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("sid")] public string SymbolId { get; set; }
        [JsonPropertyName("symbol")] public string Symbol { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("fiat")] public bool Fiat { get; set; }
        [JsonPropertyName("route")] public string Route { get; set; }
    }
}