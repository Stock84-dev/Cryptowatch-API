namespace CryptoWatch.API.Types;

/// <summary>
///     An asset can be a crypto or fiat currency.
/// </summary>
public class Assets
{
    public int id { get; set; }
    public string symbol { get; set; }
    public string name { get; set; }
    public bool fiat { get; set; }
    public string route { get; set; }
}