using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct Pairs
{
    [JsonConstructor]
    public Pairs(ResultDetails[] result, Cursor cursor, Allowance allowance)
    {
        Result = result;
        Cursor = cursor;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultDetails[] Result { get; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public struct ResultDetails
    {
        [JsonConstructor]
        public ResultDetails(
            int id,
            string symbol,
            Asset @base,
            Asset quote,
            string route,
            string futuresContractPeriod
        )
        {
            Id = id;
            Symbol = symbol;
            Base = @base;
            Quote = quote;
            Route = route;
            FuturesContractPeriod = futuresContractPeriod;
        }

        [JsonPropertyName("id")] public int Id { get; }
        [JsonPropertyName("symbol")] public string Symbol { get; }
        [JsonPropertyName("base")] public Asset Base { get; }
        [JsonPropertyName("quote")] public Asset Quote { get; }
        [JsonPropertyName("route")] public string Route { get; }

        [JsonPropertyName("futuresContractPeriod")]
        public string FuturesContractPeriod { get; }
    }
}
