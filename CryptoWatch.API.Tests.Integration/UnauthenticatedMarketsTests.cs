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
        _cryptoWatchServer.SetupMarketsApi();

        var marketListing = await new CryptoWatchApi(_httpClientFactory.Object).Markets.ListAsyncTask();

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
}