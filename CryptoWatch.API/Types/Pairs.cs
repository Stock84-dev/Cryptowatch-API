using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

/// <summary>
///     A pair of assets. Each pair has a base and a quote. For example, btceur has base btc and quote eur.
/// </summary>
public struct Pairs
{
    public string symbol { get; set; }
    public int id { get; set; }
    [JsonPropertyName("base")] public Base basePair { get; set; }
    [JsonPropertyName("quote")] public Quote quotePair { get; set; }
    public string route { get; set; }

    /// <summary>
    ///     Not always set.
    /// </summary>
    public string futuresContractPeriod { get; set; }
}