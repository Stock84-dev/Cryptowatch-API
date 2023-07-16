using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public struct Candlestick
{
    private Candlestick(long closeTime, double openPrice, double highPrice, double lowPrice, double closePrice,
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

    public static Candlestick Deserialize()
    {
        return new Candlestick(0, 0, 0, 0, 0, 0);
    }

    private struct SerializedCandlestick
    {
        public double[][] AllCandlesticks { get; set; }

        [JsonPropertyName("60")]
        private double[][] Min
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("180")]
        private double[][] _180
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("300")]
        private double[][] _300
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("900")]
        private double[][] _900
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("1800")]
        private double[][] _1800
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("3600")]
        private double[][] _3600
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("7200")]
        private double[][] _7200
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("14400")]
        private double[][] _14400
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("21600")]
        private double[][] _21600
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("43200")]
        private double[][] _43200
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("86400")]
        private double[][] _86400
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("259200")]
        private double[][] _259200
        {
            set => AllCandlesticks = value;
        }

        [JsonPropertyName("604800")]
        private double[][] _604800
        {
            set => AllCandlesticks = value;
        }
    }
}