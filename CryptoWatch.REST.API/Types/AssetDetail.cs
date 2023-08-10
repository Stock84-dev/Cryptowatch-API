using System.Text.Json.Serialization;

namespace CryptoWatch.REST.API.Types;

public readonly struct AssetDetail
{
    [JsonConstructor]
    public AssetDetail(ResultDetails result, Cursor cursor, Allowance allowance)
    {
        Result = result;
        Cursor = cursor;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultDetails Result { get; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct ResultDetails
    {
        [JsonConstructor]
        public ResultDetails(int id, string symbolId, string symbol, string name, bool fiat, Markets assetMarkets)
        {
            Id = id;
            SymbolId = symbolId;
            Symbol = symbol;
            Name = name;
            Fiat = fiat;
            AssetMarkets = assetMarkets;
        }

        [JsonPropertyName("id")] public int Id { get; }
        [JsonPropertyName("sid")] public string SymbolId { get; }
        [JsonPropertyName("symbol")] public string Symbol { get; }
        [JsonPropertyName("name")] public string Name { get; }
        [JsonPropertyName("fiat")] public bool Fiat { get; }
        [JsonPropertyName("markets")] public Markets AssetMarkets { get; }
    }

    public readonly struct Markets
    {
        [JsonConstructor]
        public Markets(Base[] baseMarket, Base[] quoteMarket)
        {
            BaseMarket = baseMarket;
            QuoteMarket = quoteMarket;
        }

        [JsonPropertyName("base")] public Base[] BaseMarket { get; }
        [JsonPropertyName("quote")] public Base[] QuoteMarket { get; }
    }

    public readonly struct Base
    {
        [JsonConstructor]
        public Base(int id, string exchange, string pair, bool active, string route)
        {
            Id = id;
            Exchange = exchange;
            Pair = pair;
            Active = active;
            Route = route;
        }

        [JsonPropertyName("id")] public int Id { get; }
        [JsonPropertyName("exchange")] public string Exchange { get; }
        [JsonPropertyName("pair")] public string Pair { get; }
        [JsonPropertyName("active")] public bool Active { get; }
        [JsonPropertyName("route")] public string Route { get; }
    }
}
