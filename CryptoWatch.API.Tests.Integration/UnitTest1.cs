using Xunit;

namespace CryptoWatch.API.Tests.Integration;

public class UnitTest1
{
    private IHttpClientFactory _httpClientFactory;
    
    [Fact]
    public void Test1()
    {
        var apiConfigurations = new ApiConfiguration(_httpClientFactory);
        var cryptoWatchApi = new CryptoWatchApi(apiConfigurations);
        var list = cryptoWatchApi.Assets.List();
        // var details = cryptoWatchApi.Assets.Details();
    }
}