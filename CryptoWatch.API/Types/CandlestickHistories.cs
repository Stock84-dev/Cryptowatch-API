using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct CandlestickHistories
{
    [JsonInclude] [JsonPropertyName("result")]
    public readonly Dictionary<string, double[][]> Result;

    [JsonConstructor]
    public CandlestickHistories(Dictionary<string, double[][]> result, Allowance allowance)
    {
        Result = result;
        Allowance = allowance;
    }

    [JsonPropertyName("allowance")] public Allowance Allowance { get; }

    [JsonIgnore]
    public TimeBasedCandlestickHistory[] TimeBasedCandlestickHistories => Result.Select(x =>
        {
            x.Deconstruct(out var timeFrameStr, out var candles);
            
            if (timeFrameStr is "604800_Monday")
                return new TimeBasedCandlestickHistory(
                    TimeFrame.mondayWeek, candles
                        .Select(candle => new OpenHighLowCloseCandle(candle))
                        .ToArray()
                );
            
            if (Enum.TryParse<TimeFrame>(timeFrameStr, out var timeFrame))
                return new TimeBasedCandlestickHistory(
                    timeFrame,
                    candles.Select(candle => new OpenHighLowCloseCandle(candle))
                        .ToArray()
                );

            throw new InvalidEnumArgumentException(timeFrameStr);
        })
        .ToArray();

    public readonly struct TimeBasedCandlestickHistory
    {
        public TimeBasedCandlestickHistory(TimeFrame timeFrame, OpenHighLowCloseCandle[] openHighLowCloseCandles)
        {
            TimeFrame = timeFrame;
            OpenHighLowCloseCandles = openHighLowCloseCandles;
        }

        public TimeFrame TimeFrame { get; }
        public OpenHighLowCloseCandle[] OpenHighLowCloseCandles { get; }
    }

    public readonly struct OpenHighLowCloseCandle
    {
        public OpenHighLowCloseCandle(IReadOnlyList<double> ohlc)
        {
            CloseTime = DateTimeOffset.FromUnixTimeSeconds((long)ohlc[0]);
            OpenPrice = ohlc[1];
            HighPrice = ohlc[2];
            LowPrice = ohlc[3];
            ClosePrice = ohlc[4];
            Volume = ohlc[5];
            QuoteVolume = ohlc[6];
        }

        public DateTimeOffset CloseTime { get; }
        public double OpenPrice { get; }
        public double HighPrice { get; }
        public double LowPrice { get; }
        public double ClosePrice { get; }
        public double Volume { get; }
        public double QuoteVolume { get; }
    }
}
