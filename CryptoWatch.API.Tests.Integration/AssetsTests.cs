using System.Net;
using FluentAssertions;
using Moq;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public class AssetsTests : IAsyncLifetime
{
    private readonly CryptoWatchServerApi _cryptoWatchServer = new();
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();

    public AssetsTests()
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
        _cryptoWatchServer.SetupAssetsApi();
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        _cryptoWatchServer.Dispose();

        return Task.CompletedTask;
    }

    [Fact]
    public async Task Asserts_CryptoWatchApiAssetsListing_HttpResponse()
    {
        var apiConfigurations = new ApiConfiguration(_httpClientFactory.Object);
        var cryptoWatchApi = new CryptoWatchApi(apiConfigurations);
        var list = await cryptoWatchApi.Assets.List();

        list.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        list.ReasonPhrase
            .Should()
            .Match("OK");
        list.Version
            .Should()
            .Be(new Version(1, 1));
        list.Headers.Date
            .Should()
            .NotBeNull();
        list.Headers.Date!.Value.Date
            .Should()
            .Be(DateTimeOffset.UtcNow.Date);
        list.Headers
            .Should()
            .ContainKeys("Date", "Transfer-Encoding", "Access-Control-Allow-Headers",
                "Referrer-Policy", "Vary", "X-Content-Type-Options", "CF-Cache-Status", "Set-Cookie",
                "Strict-Transport-Security", "Server", "CF-RAY");
        list.Content.Headers
            .Should()
            .ContainKeys("Content-Type", "Content-Encoding");
        list.RequestMessage!.RequestUri!.LocalPath
            .Should()
            .Match("/assets");
    }
}