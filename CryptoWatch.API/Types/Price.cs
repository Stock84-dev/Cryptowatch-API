namespace CryptoWatch.API.Types;

public struct Price
{
    public double last { get; set; }
    public double high { get; set; }
    public double low { get; set; }
    public Change change { get; set; }
}