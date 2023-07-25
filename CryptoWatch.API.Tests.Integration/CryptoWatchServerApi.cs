using System.Net;
using CryptoWatch.API.Tests.Integration.Resources;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace CryptoWatch.API.Tests.Integration;

public class CryptoWatchServerApi : IDisposable
{
    private const string AssetsRoute = "/assets";
    private const string ExchangesRoute = "/exchanges";
    private const string MarketsRoute = "/markets";
    private const string PairsRoute = "/pairs";

    private static readonly Dictionary<string, string> DefaultCurlHeaders = new()
    {
        { "Date", $"{DateTimeOffset.UtcNow:R}" },
        { "Content-Type", "application/json" },
        { "Transfer-Encoding", "chunked" },
        { "Connection", "keep-alive" },
        {
            "Access-Control-Allow-Headers",
            "X-CW-Integration, X-CW-Very-Secret-Value, sentry-trace, baggage"
        },
        { "Referrer-Policy", "origin-when-cross-origin" },
        { "Vary", "Origin" },
        { "X-Content-Type-Options", "nosniff" },
        { "CF-Cache-Status", "DYNAMIC" },
        {
            "Set-Cookie",
            $"__cf_bm=wt6xdEv77_sS6JI.guXkxZKmscvJccneOteen5inHbk-1689542572-0-AVxgdUGrcAMkwslGPwufsXb7eIuej/fJNjUMgvpth/wCVzjoBWwR0zkZE+HttkSBr/Y38TFq3d6thfEfA9JwPec=; path=/; expires={DateTimeOffset.UtcNow:R}; domain=.cryptowat.ch; HttpOnly; Secure; SameSite=None"
        },
        { "Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload" },
        { "Server", "cloudflare" },
        { "CF-RAY", "7e7d4d9699bb539b-SSA" },
        { "Content-Encoding", "br" }
    };

    private readonly WireMockServer _wireMockServer;

    public CryptoWatchServerApi() => _wireMockServer = WireMockServer.Start();

    public string Url => _wireMockServer.Url!;

    public void Dispose()
    {
        _wireMockServer.Stop();
        _wireMockServer.Dispose();
    }

    public void SetupHeaderAuthenticatedAssetsDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath(AssetsRoute))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsAuthenticatedMockedResponses.DefaultListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupHeaderInvalidlyAuthenticatedAssetsDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "---")
                .WithPath(AssetsRoute))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsAuthenticatedMockedResponses.DefaultListingResponse)
                .WithStatusCode(HttpStatusCode.BadRequest));

    public void SetupHeaderAuthenticatedAssetsSpecificAmountListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath(AssetsRoute)
                .WithParam("limit", "5"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsAuthenticatedMockedResponses.DefaultSpecificAmountListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupHeaderAuthenticatedAssetDetailRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath($"{AssetsRoute}/btc"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsAuthenticatedMockedResponses.DetailResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupHeaderAuthenticatedAssetSpecificAmountDetailRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath($"{AssetsRoute}/btc")
                .WithParam("limit", "5"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsAuthenticatedMockedResponses.DetailSpecificAmountListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupAuthenticatedExchangesDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath(ExchangesRoute))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(ExchangesAuthenticatedMockedResponses.ExchangesListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupAuthenticatedExchangesDefaultKrakenDetailingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath($"{ExchangesRoute}/bitfinex"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(ExchangesAuthenticatedMockedResponses.SpecifiedExchangeDetailingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedAssetsDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(AssetsRoute))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsUnauthenticatedMockedResponses.DefaultListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedAssetsSpecificAmountListingRestEndpoint(uint limit) =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(AssetsRoute)
                .WithParam("limit", "5"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsUnauthenticatedMockedResponses.SpecificAmountListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedExchangesDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(ExchangesRoute))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(ExchangesUnauthenticatedMockedResponses.DefaultListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedExchangesDefaultKrakenDetailingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath($"{ExchangesRoute}/kraken"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(ExchangesUnauthenticatedMockedResponses.DefaultKrakenDetailResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedDefaultListingMarketsRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .WithPath(MarketsRoute)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(MarketsUnauthenticatedMockedResponses.DefaultListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedMarketsListingFromCursorRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(MarketsRoute)
                .WithParam("cursor", "TF8j1fnzBNxi7bQkOQgcFb2r9X_jzp0jq8PmiYcAnGzjlUHY93Sg7AdMzlzpvQ"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(MarketsUnauthenticatedMockedResponses.ListingFromCursorResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedThreeMarketsListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(MarketsRoute)
                .WithParam("limit", "3"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(MarketsUnauthenticatedMockedResponses.ListThreeMarketsResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedThreeMarketsWithCursorListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(MarketsRoute)
                .WithParam("limit", "3")
                .WithParam("cursor", "SdgMYB9J1JiK7ejL21NoCqHcRT1eb6tTAIXZ12jGbKzEiPa-xpLZOg"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(MarketsUnauthenticatedMockedResponses.ListThreeMarketsFromCursorResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairMarketDetailRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath($"{MarketsRoute}/kraken/btcusd"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(MarketsUnauthenticatedMockedResponses.PairMarketDetailResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairMarketDetailPriceRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath($"{MarketsRoute}/kraken/btcusd/price"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(MarketsUnauthenticatedMockedResponses.PairMarketPriceDetailResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedMarketsPricesRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath($"{MarketsRoute}/prices"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(MarketsUnauthenticatedMockedResponses.MarketsPricesResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedMarketsPricesWithCursorRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath($"{MarketsRoute}/prices")
                .WithParam("cursor", "BDj0fwwHBUM7Rz4YNJvyhM1vMO5PyygjB-AAht0UbizZZ7_VqEB1JA"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(MarketsUnauthenticatedMockedResponses.MarketsPricesWithCursorResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedMarketsPricesWithCursorAndLimitOfThreeRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath($"{MarketsRoute}/prices")
                .WithParam("cursor", "BDj0fwwHBUM7Rz4YNJvyhM1vMO5PyygjB-AAht0UbizZZ7_VqEB1JA")
                .WithParam("limit", "3"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(MarketsUnauthenticatedMockedResponses.MarketsPricesWithCursorResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairsDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(PairsRoute))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(PairsUnauthenticatedMockedResponses.DefaultListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairsSpecificAmountListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(PairsRoute)
                .WithParam("limit", "5"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(PairsUnauthenticatedMockedResponses.SpecificAmountListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairsListingWithCursorRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(PairsRoute)
                .WithParam("cursor", "S_v4gQoCByt1snk8oSuh670Q_QU1ZRSDlA9igxjER8lWsXXj6geogA"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(PairsUnauthenticatedMockedResponses.ListingWithCursorResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairsSpecificAmountWithCursorListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(PairsRoute)
                .WithParam("limit", "2")
                .WithParam("cursor", "S_v4gQoCByt1snk8oSuh670Q_QU1ZRSDlA9igxjER8lWsXXj6geogA"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(PairsUnauthenticatedMockedResponses.SpecificAmountListingWithCursorResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairsDefaultDetailRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath($"{PairsRoute}/0neweth"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(PairsUnauthenticatedMockedResponses.DetailResponse)
                .WithStatusCode(HttpStatusCode.OK));
}