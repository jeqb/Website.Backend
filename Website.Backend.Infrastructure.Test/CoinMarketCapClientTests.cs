using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Website.Backend.Infrastructure.Test
{
    public class CoinMarketCapClientTests
    {
        private readonly NullLogger<CoinMarketCapClient> _nullLogger;

        private readonly HttpClient _httpClient;

        private readonly string _apiKey;

        public CoinMarketCapClientTests()
        {
            _nullLogger = new();

            _httpClient = new();
            
            // DO NOT COMMIT TO VERSION CONTROL
            _apiKey = "";
        }

        [Fact]
        public async Task GetBtcPriceInUsd_GivenProperCredentials_ReturnsValue()
        {
            // Arrange

            CoinMarketCapClient target = new(_nullLogger, _httpClient, _apiKey);


            // Act

            decimal result = await target.GetBtcPriceInUsd();


            // Assert

            Assert.True(result > 0);
        }

        /// <summary>
        /// This will make the HttpResponseMessage.IsSuccessStatusCode != true
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetBtcPriceInUsd_GivenBadKey_ReturnsZero()
        {
            // Arrange

            string badKey = "";

            CoinMarketCapClient target = new(_nullLogger, _httpClient, badKey);


            // Act

            decimal result = await target.GetBtcPriceInUsd();


            // Assert

            Assert.True(result == 0);
        }
    }
}
