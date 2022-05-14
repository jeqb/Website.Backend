using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using Website.Backend.Infrastructure.Finance;
using Xunit;

namespace Website.Backend.Infrastructure.Test
{
    public class MarketWatchScraperTests
    {
        private readonly NullLogger<MarketWatchScraper> _nullLogger;

        private readonly HttpClient _httpClient;

        public MarketWatchScraperTests()
        {
            _nullLogger = new();

            _httpClient = new();
        }

        [Fact]
        public async Task GetSp500PriceChangePercent_WhenCalled_ReturnsNonZeroValue()
        {
            // Arrange

            MarketWatchScraper target = new(_nullLogger, _httpClient);


            // Act

            double result = await target.GetSp500PriceChangePercent();


            // Assert

            Assert.True(result != 0);
        }
    }
}
