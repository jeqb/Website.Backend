using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace Website.Backend.Infrastructure.Finance
{
    /// <summary>
    /// Scrapes MarketWatch.com for Stock Market type of data.
    /// </summary>
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

                double priceChangePercent;

                try
                {
                    priceChangePercent = ParseHtml(responseContent);
                }
                catch (Exception ex)
                {
                    // if this happens, the source HTML probably changed.
                    _logger.LogInformation("MarketWatchScraper could not parse the returned HTML");

                    priceChangePercent = 0;
                }

                return priceChangePercent;
            }
        }

        /// <summary>
        /// Specific to S&P500 page right now. could change in future.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
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
