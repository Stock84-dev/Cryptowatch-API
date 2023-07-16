namespace CryptoWatch.API.Types;

public struct SiteInformation
{
    public string revision { get; set; }
    public string uptime { get; set; }
    public string documentation { get; set; }
    public string[] indexes { get; set; }
}