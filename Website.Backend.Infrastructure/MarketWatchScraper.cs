using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Website.Backend.Infrastructure.Interfaces;

namespace Website.Backend.Infrastructure
{
    public class MarketWatchScraper : IStockMarketService
    {
        private const string _sp500Url = "https://www.marketwatch.com/investing/index/spx";

        private readonly ILogger<MarketWatchScraper> _logger;

        private readonly HttpClient _httpClient;

        public MarketWatchScraper(ILogger<MarketWatchScraper> logger, HttpClient httpClient)
        {
            _logger = logger;

            _httpClient = httpClient;
        }

        public async Task<double> GetSp500PriceChangePercent()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_sp500Url);

            if (response.IsSuccessStatusCode != true)
            {
                _logger.LogInformation("MarketWatchScraper could not retrieve S&P 500 data.");

                return 0;
            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                double priceChangePercent = ParseHtml(responseContent);

                return priceChangePercent;
            }
        }

        private double ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // it looks like this:
            // <meta name="priceChangePercent" content="-2.77%" />
            string priceChangeString = htmlDoc.DocumentNode
                .SelectSingleNode("//meta[@name='priceChangePercent']")
                .Attributes["content"].Value;

            priceChangeString = priceChangeString.Replace("%", "");

            double price = double.Parse(priceChangeString);

            return price;
        }
    }
}
