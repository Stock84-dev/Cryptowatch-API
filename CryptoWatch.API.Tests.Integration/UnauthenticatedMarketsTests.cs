using CryptoWatch.API.Types;
using FluentAssertions;
using Moq;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public class UnauthenticatedMarketsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();

    public UnauthenticatedMarketsTests()
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
    public async Task Asserts_MarketsListing_JsonResponseDeserialization()
    {
        _cryptoWatchServer.SetupUnauthenticatedDefaultListingMarketsRestEndpoint();

        var marketListing = await new CryptoWatchApi(_httpClientFactory.Object).Markets.ListAsync();

        marketListing.Should()
            .BeOfType<MarketCollection>();
        marketListing.Result.Should()
            .BeOfType<List<MarketDetails>>();
        marketListing.Result.First()
            .Should()
            .BeOfType<MarketDetails>();
        marketListing.Result.Should()
            .HaveCount(20_000);
        marketListing.Result.First()
            .Active.Should()
            .BeTrue();
        marketListing.Result.First()
            .Exchange.Should()
            .Be("bitfinex");
        marketListing.Result.First()
            .Id.Should()
            .Be(1);
        marketListing.Result.First()
            .Pair.Should()
            .Be("btcusd");
        marketListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/bitfinex/btcusd");
        marketListing.Cursor.Should()
            .BeOfType<Cursor>();
        marketListing.Cursor.HasMore.Should()
            .BeTrue();
        marketListing.Cursor.Last.Should()
            .Be("TF8j1fnzBNxi7bQkOQgcFb2r9X_jzp0jq8PmiYcAnGzjlUHY93Sg7AdMzlzpvQ");
        marketListing.Allowance.Should()
            .BeOfType<Allowance>();
        marketListing.Allowance.Cost.Should()
            .Be(0.003M);
        marketListing.Allowance.Remaining.Should()
            .Be(9.997M);
        marketListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }

    [Fact]
    public async Task Asserts_MarketsListingFromCursor_JsonResponseDeserialization()
    {
        const string cursor = "TF8j1fnzBNxi7bQkOQgcFb2r9X_jzp0jq8PmiYcAnGzjlUHY93Sg7AdMzlzpvQ";
        _cryptoWatchServer.SetupUnauthenticatedMarketsListingFromCursorRestEndpoint();

        var marketListing = await new CryptoWatchApi(_httpClientFactory.Object).Markets.ListAsync(cursor);

        marketListing.Should()
            .BeOfType<MarketCollection>();
        marketListing.Result.Should()
            .BeOfType<List<MarketDetails>>();
        marketListing.Result.First()
            .Should()
            .BeOfType<MarketDetails>();
        marketListing.Result.Should()
            .HaveCount(7_651);
        marketListing.Result.First()
            .Active.Should()
            .BeTrue();
        marketListing.Result.First()
            .Exchange.Should()
            .Be("kraken");
        marketListing.Result.First()
            .Id.Should()
            .Be(3_025_661);
        marketListing.Result.First()
            .Pair.Should()
            .Be("egldusd");
        marketListing.Result.First()
            .Route.Should()
            .Be("https://api.cryptowat.ch/markets/kraken/egldusd");
        marketListing.Cursor.Should()
            .BeOfType<Cursor>();
        marketListing.Cursor.HasMore.Should()
            .BeFalse();
        marketListing.Cursor.Last.Should()
            .Be("BWgumN0awa71xjUYPqlzbIDvzN7sCIa85I9A4z-TUK-2amfy9co0x5cCw3ojbQ");
        marketListing.Allowance.Should()
            .BeOfType<Allowance>();
        marketListing.Allowance.Cost.Should()
            .Be(0.003M);
        marketListing.Allowance.Remaining.Should()
            .Be(9.997M);
        marketListing.Allowance.Upgrade.Should()
            .Be("For unlimited API access, create an account at https://cryptowat.ch");
    }
}