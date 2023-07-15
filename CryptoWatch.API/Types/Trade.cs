namespace CryptoWatch.API.Types;

public class Trade
{
    public Trade(int id, long timestamp, double price, double amount)
    {
        this.id = id;
        this.timestamp = timestamp;
        this.price = price;
        this.amount = amount;
    }

    public int id { get; set; }
    public long timestamp { get; set; }
    public double price { get; set; }
    public double amount { get; set; }
}