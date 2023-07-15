namespace CryptoWatch.API.Types;

/// <summary>
///     Exchanges are where all the action happens!
/// </summary>
public class Exchange
{
    public string symbol { get; set; }
    public string name { get; set; }
    public bool active { get; set; }
    public Route routes { get; set; }
}