using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Website.Backend.Infrastructure.Test
{
    public class KitcoGoldPriceScraperTests
    {
        [Fact]
        public async Task GetSpotPriceInUsd_WhenCalled_ReturnsNonZero()
        {
            // Arrange

            HttpClient httpClient = new HttpClient();

            KitcoGoldSpotPriceScraper target = new(httpClient);


            // Act

            decimal price = await target.GetSpotPriceInUsd();


            // Assert

            Assert.True(price > 0);
        }
    }
}