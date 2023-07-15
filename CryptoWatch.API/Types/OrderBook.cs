namespace CryptoWatch.API.Types;

public class OrderBook
{
    public OrderBook()
    {
        bids = new List<Order>();
        asks = new List<Order>();
    }

    public List<Order> bids { get; set; }
    public List<Order> asks { get; set; }
}