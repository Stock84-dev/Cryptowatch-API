using CryptoWatch.API.Types;
using FluentAssertions;
using Moq;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public class UnauthenticatedExchangesTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();

    public UnauthenticatedExchangesTests()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_cryptoWatchServer.Url)
        };

        _httpClientFactory.Setup(x => x.CreateClient(string.Empty))
            .Returns(httpClient);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        _cryptoWatchServer.Dispose();

        return Task.CompletedTask;
    }

    [Fact]
    public async Task Asserts_ExchangesListing_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedExchangesDefaultListingRestEndpoint();

        var exchangeDefaultListing = await new CryptoWatchApi(_httpClientFactory.Object).Exchanges.List();

        exchangeDefaultListing.Result.Should()
            .BeOfType<List<Exchanges.ResultCollection>>();
        exchangeDefaultListing.Result.Should()
            .HaveCount(48);
        exchangeDefaultListing.Result.First()
            .Id.Should()
            .Be(1);
        exchangeDefaultListing.Result.First()
            .Active.Should()
            .BeTrue();
        exchangeDefaultListing.Result.First()
            .Name.Should()
            .Be("Bitfinex");
        exchangeDefaultListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/exchanges/bitfinex");
        exchangeDefaultListing.Result.First()
            .Symbol.Should()
            .Be("bitfinex");
        exchangeDefaultListing.Allowance.Should()
            .BeOfType<Allowance>();
        exchangeDefaultListing.Allowance.Cost.Should()
            .Be(0.002M);
        exchangeDefaultListing.Allowance.Remaining.Should()
            .Be(9.998M);
        exchangeDefaultListing.Allowance.RemainingPaid.Should()
            .Be(0);
        exchangeDefaultListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_ExchangeDetailing_JsonResponseDeserialization()
    {
        const string exchange = "kraken";
        _cryptoWatchServer.SetupUnauthenticatedExchangesDefaultKrakenDetailingRestEndpoint();

        var exchangeDefaultDetailing = await new CryptoWatchApi(_httpClientFactory.Object).Exchanges.Details(exchange);

        exchangeDefaultDetailing.Result.Should()
            .BeOfType<Exchange.ResultDetail>();
        exchangeDefaultDetailing.Result.Id.Should()
            .Be(4);
        exchangeDefaultDetailing.Result.Active.Should()
            .BeTrue();
        exchangeDefaultDetailing.Result.Name.Should()
            .Be("Kraken");
        exchangeDefaultDetailing.Result.Routes.Should()
            .BeOfType<Route>();
        exchangeDefaultDetailing.Result.Routes.Markets.Should()
            .Be("https://api.cryptowat.ch/markets/kraken");
        exchangeDefaultDetailing.Result.Symbol.Should()
            .Be("kraken");
        exchangeDefaultDetailing.Allowance.Should()
            .BeOfType<Allowance>();
        exchangeDefaultDetailing.Allowance.Cost.Should()
            .Be(0.002M);
        exchangeDefaultDetailing.Allowance.Remaining.Should()
            .Be(9.996M);
        exchangeDefaultDetailing.Allowance.RemainingPaid.Should()
            .Be(0);
        exchangeDefaultDetailing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }
}