namespace CryptoWatch.API.Types;

public class SiteInformation
{
    public string revision { get; set; }
    public string uptime { get; set; }
    public string documentation { get; set; }
    public string[] indexes { get; set; }
}