using CryptoWatch.REST.API;
using CryptoWatch.REST.API.Types;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public class AuthenticatedExchangesTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();

    public AuthenticatedExchangesTests() =>
        _httpClientFactory.CreateClient(string.Empty)
            .Returns(new HttpClient
            {
                BaseAddress = new Uri(_cryptoWatchServer.Url),
                DefaultRequestHeaders = { { "X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF" } }
            });

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        _cryptoWatchServer.Dispose();

        return Task.CompletedTask;
    }

    [Fact]
    public async Task Asserts_ExchangesListing_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupAuthenticatedExchangesDefaultListingRestEndpoint();

        var exchangeDefaultListing = await new CryptoWatchApi(_httpClientFactory).Exchanges.ListAsync();

        exchangeDefaultListing.Should()
            .BeOfType<Exchanges>();
        exchangeDefaultListing.Result.Should()
            .BeOfType<Exchanges.ResultDetails[]>();
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
            .Be(0.000M);
        exchangeDefaultListing.Allowance.Remaining.Should()
            .Be(10.000M);
        exchangeDefaultListing.Allowance.RemainingPaid.Should()
            .Be(9999999999UL);
        exchangeDefaultListing.Allowance.Upgrade.Should()
            .BeNull();
    }

    [Fact]
    public async Task Asserts_ExchangeDetailing_JsonResponseDeserialization()
    {
        const string exchange = "bitfinex";
        _cryptoWatchServer.SetupAuthenticatedExchangesDefaultKrakenDetailingRestEndpoint();

        var exchangeDefaultDetailing =
            await new CryptoWatchApi(_httpClientFactory).Exchanges.DetailsAsync(exchange);

        exchangeDefaultDetailing.Result.Should()
            .BeOfType<Exchange.ResultDetail>();
        exchangeDefaultDetailing.Result.Id.Should()
            .Be(1);
        exchangeDefaultDetailing.Result.Active.Should()
            .BeTrue();
        exchangeDefaultDetailing.Result.Name.Should()
            .Be("Bitfinex");
        exchangeDefaultDetailing.Result.Routes.Should()
            .BeOfType<Route>();
        exchangeDefaultDetailing.Result.Routes.Markets.Should()
            .Be("https://api.cryptowat.ch/markets/bitfinex");
        exchangeDefaultDetailing.Result.Symbol.Should()
            .Be("bitfinex");
        exchangeDefaultDetailing.Allowance.Should()
            .BeOfType<Allowance>();
        exchangeDefaultDetailing.Allowance.Cost.Should()
            .Be(0.000M);
        exchangeDefaultDetailing.Allowance.Remaining.Should()
            .Be(10.000M);
        exchangeDefaultDetailing.Allowance.RemainingPaid.Should()
            .Be(9999999999UL);
        exchangeDefaultDetailing.Allowance.Upgrade.Should()
            .BeNull();
    }
}
