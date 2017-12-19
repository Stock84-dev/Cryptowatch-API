using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cryptowatch
{
	public class SiteInformation
	{
		public string revision { get; set; }
		public string uptime { get; set; }
		public string documentation { get; set; }
		public string[] indexes { get; set; }
	}

	/// <summary>
	/// An asset can be a crypto or fiat currency. 
	/// </summary>
	public class Assets
	{
		public int id { get; set; }
		public string symbol { get; set; }
		public string name { get; set; }
		public bool fiat { get; set; }
		public string route { get; set; }
	}

	/// <summary>
	/// An asset can be a crypto or fiat currency. 
	/// </summary>
	public class Asset
	{
		public int id { get; set; }
		public string symbol { get; set; }
		public string name { get; set; }
		public bool fiat { get; set; }
		public Markets1 markets { get; set; }
	}

	/// <summary>
	/// A pair of assets. Each pair has a base and a quote. For example, btceur has base btc and quote eur.
	/// </summary>
	public class Pairs
	{
		public string symbol { get; set; }
		public int id { get; set; }
		[JsonProperty("base")]
		public Base basePair { get; set; }
		[JsonProperty("quote")]
		public Quote quotePair { get; set; }
		public string route { get; set; }
		/// <summary>
		/// Not always set.
		/// </summary>
		public string futuresContractPeriod { get; set; }
	}

	public class Pair
	{
		public string symbol { get; set; }
		public int id { get; set; }
		[JsonProperty("base")]
		public Base basePair { get; set; }
		[JsonProperty("quote")]
		public Quote quotePair { get; set; }
		public string route { get; set; }
		public Markets[] markets { get; set; }
	}

	/// <summary>
	/// Exchanges are where all the action happens!
	/// </summary>
	public class Exchanges
	{
		public string symbol { get; set; }
		public string name { get; set; }
		public string route { get; set; }
		public bool active { get; set; }
	}

	/// <summary>
	/// Exchanges are where all the action happens!
	/// </summary>
	public class Exchange
	{
		public string symbol { get; set; }
		public string name { get; set; }
		public bool active { get; set; }
		public Route routes { get; set; }
	}

	/// <summary>
	/// A market is a pair listed on an exchange. For example, pair btceur on exchange kraken is a market.
	/// </summary>
	public class Markets
	{
		public int id { get; set; }
		public string exchange { get; set; }
		public string pair { get; set; }
		public bool active { get; set; }
		public string route { get; set; }
	}

	/// <summary>
	/// A market is a pair listed on an exchange. For example, pair btceur on exchange kraken is a market.
	/// </summary>
	public class Market
	{
		public string exchange { get; set; }
		public string pair { get; set; }
		public bool active { get; set; }
		public Routes routes { get; set; }
	}

	public class Markets1
	{
		[JsonProperty("base")]
		public Bases[] baseMarket { get; set; }
		[JsonProperty("quote")]
		public Quotes[] quoteMarket { get; set; }
	}

	public class Bases
	{
		public int id { get; set; }
		public string exchange { get; set; }
		public string pair { get; set; }
		public bool active { get; set; }
		public string route { get; set; }
	}
	// TODO: concatenate base/s and qoute/s classes
	public class Base
	{
		public int id { get; set; }
		public string route { get; set; }
		public string symbol { get; set; }
		public string name { get; set; }
		public bool fiat { get; set; }
	}

	public class Quotes
	{
		public int id { get; set; }
		public string exchange { get; set; }
		public string pair { get; set; }
		public bool active { get; set; }
		public string route { get; set; }
	}

	public class Quote
	{
		public int id { get; set; }
		public string route { get; set; }
		public string symbol { get; set; }
		public string name { get; set; }
		public bool fiat { get; set; }
	}

	public class Routes
	{
		public string price { get; set; }
		public string summary { get; set; }
		public string orderbook { get; set; }
		public string trades { get; set; }
		public string ohlc { get; set; }
	}

	public class Route
	{
		public string markets { get; set; }
	}

	public class Summary
	{
		public Price price { get; set; }
		public double volume { get; set; }
	}

	public class Price
	{
		public double last { get; set; }
		public double high { get; set; }
		public double low { get; set; }
		public Change change { get; set; }
	}

	public class Change
	{
		public double percentage { get; set; }
		public double absolute { get; set; }
	}

	public class Trade
	{
		public int id { get; set; }
		public long timestamp { get; set; }
		public double price { get; set; }
		public double amount { get; set; }

		public Trade(int id, long timestamp, double price, double amount)
		{
			this.id = id;
			this.timestamp = timestamp;
			this.price = price;
			this.amount = amount;
		}
	}

	public class OrderBook
	{
		public List<Order> bids { get; set; }
		public List<Order> asks { get; set; }

		public OrderBook()
		{
			bids = new List<Order>();
			asks = new List<Order>();
		}
	}

	public class Order
	{
		public double price;
		public double amount;

		public Order(double price, double amount)
		{
			this.price = price;
			this.amount = amount;
		}
	}

	public enum TimeFrame { min1 = 60, min3 = 180, min5 = 300, min15 = 900, min30 = 1800, h1 = 3600, h2 = 7200, h4 = 14400, h6 = 21600, h12 = 43200, d1 = 86400, d3 = 259200, w1 = 604800 }

	public class Candlestick
	{
		public long closeTime;
		public double openPrice;
		public double highPrice;
		public double lowPrice;
		public double closePrice;
		public double volume;

		public Candlestick(long closeTime, double openPrice, double highPrice, double lowPrice, double closePrice, double volume)
		{
			this.closeTime = closeTime;
			this.openPrice = openPrice;
			this.highPrice = highPrice;
			this.lowPrice = lowPrice;
			this.closePrice = closePrice;
			this.volume = volume;
		}
	}

	public class Allowance
	{
		public long cost { get; set; }
		public long remaining { get; set; }
	}

	/// <summary>
	/// The exception that is thrown when calling a method, but remaining allowance is zero.
	/// </summary>
	public class OutOfAllowanceException : Exception
	{
		public OutOfAllowanceException(string message) : base(message)
		{

		}
	}
}





