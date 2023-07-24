namespace CryptoWatch.API.Types;

public struct Routes
{
    public string price { get; set; }
    public string summary { get; set; }
    public string orderbook { get; set; }
    public string trades { get; set; }
    public string ohlc { get; set; }
}