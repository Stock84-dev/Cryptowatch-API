using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct AssetDetail
{
    [JsonConstructor]
    public AssetDetail(ResultCollection result, Cursor cursor, Allowance allowance)
    {
        Result = result;
        Cursor = cursor;
        Allowance = allowance;
    }

    [JsonPropertyName("result")] public ResultCollection Result { get; }
    [JsonPropertyName("cursor")] public Cursor Cursor { get; }
    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    public readonly struct ResultCollection
    {
        [JsonConstructor]
        public ResultCollection(int id, string symbolId, string symbol, string name, bool fiat, Markets assetMarkets)
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
        public Markets(List<Bases> baseMarket, List<Bases> quoteMarket)
        {
            BaseMarket = baseMarket;
            QuoteMarket = quoteMarket;
        }

        [JsonPropertyName("base")] public List<Bases> BaseMarket { get; }
        [JsonPropertyName("quote")] public List<Bases> QuoteMarket { get; }
    }

    public readonly struct Bases
    {
        [JsonConstructor]
        public Bases(int id, string exchange, string pair, bool active, string route)
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
