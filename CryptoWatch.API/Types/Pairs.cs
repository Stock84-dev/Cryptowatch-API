using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Pairs
{
    [JsonPropertyName("result")] public List<ResultCollection> Result { get; set; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; set; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; set; }

    public struct ResultCollection
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("symbol")] public string Symbol { get; set; }
        [JsonPropertyName("base")] public Base BasePair { get; set; }
        [JsonPropertyName("quote")] public Quote QuotePair { get; set; }
        [JsonPropertyName("route")] public string Route { get; set; }

        [JsonPropertyName("futuresContractPeriod")]
        public string FuturesContractPeriod { get; set; }
    }
}