using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatch.REST.API;

public static class CryptoWatchApiServiceRegister
{
    public static IHttpClientBuilder AddCryptoWatchHttpClient(this IServiceCollection serviceCollection) =>
        serviceCollection.AddHttpClient<CryptoWatchRestApi>(httpClient =>
            httpClient.BaseAddress = new Uri(CryptoWatchRestApi.RootUrl));
}
