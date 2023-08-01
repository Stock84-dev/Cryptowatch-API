using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct MarketPairDetail
{
    [JsonConstructor]
    public MarketPairDetail(ResultDetails result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultDetails Result { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct ResultDetails
    {
        [JsonConstructor]
        public ResultDetails(int id, string exchange, string pair, bool active, Routes routes)
        {
            Id = id;
            Exchange = exchange;
            Pair = pair;
            Active = active;
            Routes = routes;
        }

        [JsonPropertyName("id")] public int Id { get; }
        [JsonPropertyName("exchange")] public string Exchange { get; }
        [JsonPropertyName("pair")] public string Pair { get; }
        [JsonPropertyName("active")] public bool Active { get; }
        [JsonPropertyName("routes")] public Routes Routes { get; }
    }
}
