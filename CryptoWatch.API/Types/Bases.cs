namespace CryptoWatch.API.Types;

public struct Bases
{
    public int id { get; set; }
    public string exchange { get; set; }
    public string pair { get; set; }
    public bool active { get; set; }
    public string route { get; set; }
}