using System.Runtime.Serialization;

namespace CryptoWatch.API.Types;

public class Pair
{
    public string symbol { get; set; }
    public int id { get; set; }
    [DataMember(Name = "base")] public Base basePair { get; set; }
    [DataMember(Name = "quote")] public Quote quotePair { get; set; }
    public string route { get; set; }
    public Markets[] markets { get; set; }
}