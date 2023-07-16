namespace CryptoWatch.API.Types;

public struct Order
{
    public double amount;
    public double price;

    public Order(double price, double amount)
    {
        this.price = price;
        this.amount = amount;
    }
}