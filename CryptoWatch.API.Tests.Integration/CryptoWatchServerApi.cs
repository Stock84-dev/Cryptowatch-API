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
                .WithBody(AssetsMockedResponses.AuthenticatedAssetsDefaultListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupHeaderInvalidlyAuthenticatedAssetsDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "---")
                .WithPath(AssetsRoute))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsMockedResponses.AuthenticatedAssetsDefaultListingResponse)
                .WithStatusCode(HttpStatusCode.BadRequest));

    public void SetupHeaderAuthenticatedAssetsSpecificAmountListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath(AssetsRoute)
                .WithParam("limit", "5"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsMockedResponses.AuthenticatedAssetsDefaultSpecificAmountListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupHeaderAuthenticatedAssetDetailRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath($"{AssetsRoute}/btc"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsMockedResponses.AuthenticatedAssetDetailResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupHeaderAuthenticatedAssetSpecificAmountDetailRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath($"{AssetsRoute}/btc")
                .WithParam("limit", "5"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsMockedResponses.AuthenticatedAssetDetailSpecificAmountListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupAuthenticatedExchangesDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath(ExchangesRoute))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(ExchangesMockedResponses.AuthenticatedEveryExchangeListing)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupAuthenticatedExchangesDefaultKrakenDetailingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithHeader("X-CW-API-Key", "CXRJ2EJTOLGUF4RNY4CF")
                .WithPath($"{ExchangesRoute}/bitfinex"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(ExchangesMockedResponses.AuthenticatedSpecifiedExchangeDetailing)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedAssetsDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(AssetsRoute))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsMockedResponses.UnauthenticatedAssetsDefaultListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedAssetsSpecificAmountListingRestEndpoint(uint limit) =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(AssetsRoute)
                .WithParam("limit", "5"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(AssetsMockedResponses.UnauthenticatedAssetsSpecificAmountListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedExchangesDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath(ExchangesRoute))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(ExchangesMockedResponses.UnauthenticatedExchangesDefaultListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedExchangesDefaultKrakenDetailingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath($"{ExchangesRoute}/kraken"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(ExchangesMockedResponses.UnauthenticatedExchangesDefaultKrakenDetailResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupMarketsApi() =>
        _wireMockServer.Given(Request.Create()
                .WithPath(MarketsRoute)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(MarketsMockedResponses.UnauthenticatedMarketsRootListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairsDefaultListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath("/pairs"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(PairsMockedResponses.UnauthenticatedPairsDefaultListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairsSpecificAmountListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath("/pairs")
                .WithParam("limit", "5"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(PairsMockedResponses.UnauthenticatedPairsSpecificAmountListingResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairsListingWithCursorRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath("/pairs")
                .WithParam("cursor", "S_v4gQoCByt1snk8oSuh670Q_QU1ZRSDlA9igxjER8lWsXXj6geogA"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(PairsMockedResponses.UnauthenticatedPairsListingWithCursorResponse)
                .WithStatusCode(HttpStatusCode.OK));

    public void SetupUnauthenticatedPairsSpecificAmountWithCursorListingRestEndpoint() =>
        _wireMockServer.Given(Request.Create()
                .UsingGet()
                .WithPath("/pairs")
                .WithParam("limit", "2")
                .WithParam("cursor", "S_v4gQoCByt1snk8oSuh670Q_QU1ZRSDlA9igxjER8lWsXXj6geogA"))
            .RespondWith(Response.Create()
                .WithHeaders(DefaultCurlHeaders)
                .WithBody(PairsMockedResponses.UnauthenticatedPairsSpecificAmountListingWithCursorResponse)
                .WithStatusCode(HttpStatusCode.OK));
}