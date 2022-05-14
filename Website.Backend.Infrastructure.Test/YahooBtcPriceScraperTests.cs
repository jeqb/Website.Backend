using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using Website.Backend.Infrastructure.Finance;
using Xunit;

namespace Website.Backend.Infrastructure.Test
{
    public class YahooBtcPriceScraperTests
    {
        private readonly NullLogger<YahooBtcPriceScraper> _nullLogger;

        private readonly HttpClient _httpClient;

        public YahooBtcPriceScraperTests()
        {
            _nullLogger = new();

            _httpClient = new();
        }

        [Fact]
        public async Task GetBtcPriceInUsd_WhenCalled_ReturnsNonZero()
        {
            // Arrange

            YahooBtcPriceScraper target = new(_nullLogger, _httpClient);


            // Act

            decimal result = await target.GetBtcPriceInUsd();


            // Assert

            Assert.True(result > 0);
        }
    }
}
