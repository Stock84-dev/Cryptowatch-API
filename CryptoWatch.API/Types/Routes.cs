using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct Routes
{
    [JsonConstructor]
    public Routes(string price, string summary, string orderbook, string trades, string ohlc)
    {
        Price = price;
        Summary = summary;
        Orderbook = orderbook;
        Trades = trades;
        Ohlc = ohlc;
    }

    [JsonPropertyName("price")] public string Price { get; }
    [JsonPropertyName("summary")] public string Summary { get; }
    [JsonPropertyName("orderbook")] public string Orderbook { get; }
    [JsonPropertyName("trades")] public string Trades { get; }
    [JsonPropertyName("ohlc")] public string Ohlc { get; }
}