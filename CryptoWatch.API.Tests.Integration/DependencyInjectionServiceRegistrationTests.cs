using CryptoWatch.REST.API;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public class DependencyInjectionServiceRegistrationTests
{
    [Fact]
    public void Assert_RestApiHttpClient_Registration()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddCryptoWatchHttpClient();

        var cryptoWatchRestApiFactory = () => serviceCollection.BuildServiceProvider()
            .GetRequiredService<CryptoWatchRestApi>();
        cryptoWatchRestApiFactory.Should()
            .NotThrow()
            .And.Subject.Invoke()
            .Should()
            .BeOfType<CryptoWatchRestApi>();
    }

    [Fact]
    public void Assert_Test()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCryptoWatchHttpClient();
        var cryptoWatchRestApi = serviceCollection.BuildServiceProvider().GetRequiredService<CryptoWatchRestApi>();

        cryptoWatchRestApi.Assets.ListAsync();
    }
}
