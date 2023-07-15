using System.Runtime.Serialization;

namespace CryptoWatch.API.Types;

public class Markets1
{
    [DataMember(Name = "base")] public Bases[] baseMarket { get; set; }
    [DataMember(Name = "quote")] public Quotes[] quoteMarket { get; set; }
}