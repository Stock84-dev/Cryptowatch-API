namespace CryptoWatch.API.Types;

public class Candlestick
{
    public Candlestick(long closeTime, double openPrice, double highPrice, double lowPrice, double closePrice,
        double volume)
    {
        CloseTime = closeTime;
        OpenPrice = openPrice;
        HighPrice = highPrice;
        LowPrice = lowPrice;
        ClosePrice = closePrice;
        Volume = volume;
    }

    public double ClosePrice { get; }
    public long CloseTime { get; }
    public double HighPrice { get; }
    public double LowPrice { get; }
    public double OpenPrice { get; }
    public double Volume { get; }
}