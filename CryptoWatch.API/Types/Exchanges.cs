namespace CryptoWatch.API.Types;

/// <summary>
///     Exchanges are where all the action happens!
/// </summary>
public struct Exchanges
{
    public string symbol { get; set; }
    public string name { get; set; }
    public string route { get; set; }
    public bool active { get; set; }
}