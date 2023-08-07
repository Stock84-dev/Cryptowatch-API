using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatch.API;

public static class CryptoWatchApiServiceRegister
{
    public static IHttpClientBuilder AddCryptoWatchHttpClient(this IServiceCollection serviceCollection) =>
        serviceCollection.AddHttpClient<CryptoWatchApi>(httpClient =>
            httpClient.BaseAddress = new Uri(CryptoWatchApi.RootUrl));
}
