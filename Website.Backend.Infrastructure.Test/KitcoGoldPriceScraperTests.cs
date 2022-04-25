using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Website.Backend.Infrastructure.Test
{
    public class KitcoGoldPriceScraperTests
    {
        private readonly NullLogger<KitcoGoldSpotPriceScraper> _nullLogger;

        private readonly HttpClient _httpClient;

        public KitcoGoldPriceScraperTests()
        {
            _nullLogger = new();

            _httpClient = new();
        }

        [Fact]
        public async Task GetSpotPriceInUsd_WhenCalled_ReturnsNonZero()
        {
            // Arrange

            KitcoGoldSpotPriceScraper target = new(_nullLogger, _httpClient);


            // Act

            decimal price = await target.GetSpotPriceInUsd();


            // Assert

            Assert.True(price > 0);
        }
    }
}