using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace CryptoWatch.REST.API;

public static class CryptoWatchApiServiceRegister
{
    public static IHttpClientBuilder AddCryptoWatchHttpClient(this IServiceCollection serviceCollection) =>
        serviceCollection.AddHttpClient<CryptoWatchRestApi>(httpClient =>
                httpClient.BaseAddress = new Uri(CryptoWatchRestApi.RootUrl))
            .AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode is HttpStatusCode.NotFound)
                .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(2), 6)))
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));
}
