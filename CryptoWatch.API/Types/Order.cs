using System.Text.Json.Serialization;

namespace CryptoWatch.API.Types;

public readonly struct Order
{
    [JsonConstructor]
    public Order(double price, double amount)
    {
        Price = price;
        Amount = amount;
    }

    public double Amount { get; }
    public double Price { get; }
}
