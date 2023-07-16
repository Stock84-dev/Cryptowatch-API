namespace CryptoWatch.API.Types;

/// <summary>
///     A market is a pair listed on an exchange. For example, pair btceur on exchange kraken is a market.
/// </summary>
public struct Market
{
    public string exchange { get; set; }
    public string pair { get; set; }
    public bool active { get; set; }
    public Routes routes { get; set; }
}