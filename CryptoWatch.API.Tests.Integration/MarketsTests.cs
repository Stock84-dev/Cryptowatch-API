using Moq;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public class MarketsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();

    public MarketsTests()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_cryptoWatchServer.Url)
        };

        _httpClientFactory.Setup(x => x.CreateClient(string.Empty))
            .Returns(httpClient);
    }

    public Task InitializeAsync()
    {
        _cryptoWatchServer.SetupMarketsApi();
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        _cryptoWatchServer.Dispose();

        return Task.CompletedTask;
    }

    [Fact]
    public async Task Asserts_CryptoWatchApiMarketsListing_JsonResponseDeserialization()
    {
        var list = await new CryptoWatchApi(_httpClientFactory.Object).Markets.ListAsyncTask();

        Console.WriteLine();
    }
}