using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace CryptoWatch.API.Tests.Integration;

public class CryptoWatchServerApi : IDisposable
{
    private const string AssetsRoute = "/assets";
    private readonly WireMockServer _wireMockServer;

    public CryptoWatchServerApi() => _wireMockServer = WireMockServer.Start();

    public string Url => _wireMockServer.Url!;

    public void Dispose()
    {
        _wireMockServer.Stop();
        _wireMockServer.Dispose();
    }

    public void SetupAssetsApi()
    {
        _wireMockServer.Given(Request.Create()
                .WithPath(AssetsRoute)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(MockedResponses.AssetsRootListingResponse)
                .WithHeaders(new Dictionary<string, string>
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
                })
                .WithStatusCode(HttpStatusCode.OK));
    }
}