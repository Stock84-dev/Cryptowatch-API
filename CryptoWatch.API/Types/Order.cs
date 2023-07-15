namespace CryptoWatch.API.Types;

public class Order
{
    public double amount;
    public double price;

    public Order(double price, double amount)
    {
        this.price = price;
        this.amount = amount;
    }
}