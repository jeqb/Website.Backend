using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace Website.Backend.Infrastructure.Finance
{
    public class YahooBtcPriceScraper : ICryptoCurrencyService
    {
        private const string _url = "https://finance.yahoo.com/quote/BTC-USD/";

        private readonly ILogger<YahooBtcPriceScraper> _logger;

        private readonly HttpClient _httpClient;

        public YahooBtcPriceScraper(ILogger<YahooBtcPriceScraper> logger, HttpClient httpClient)
        {
            _logger = logger;

            _httpClient = httpClient;
        }

        public async Task<decimal> GetBtcPriceInUsd()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_url);

            if (response.IsSuccessStatusCode != true)
            {
                _logger.LogInformation("YahooBtcPriceScraper could not retrieve BTC price.");

                return 0;
            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                decimal price;

                try
                {
                    price = ParseHtml(responseContent);
                }
                catch (Exception ex)
                {
                    // if this happens, the source HTML probably changed.
                    _logger.LogInformation("YahooBtcPriceScraper could not parse the returned HTML");

                    price = 0;
                }

                return price;
            }
        }

        private decimal ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // https://stackoverflow.com/questions/36711680/c-sharp-html-agility-pack-get-elements-by-class-name
            HtmlNodeCollection nodeCollection = htmlDoc.DocumentNode.SelectNodes(
                "//fin-streamer[contains(@class, 'Fw(b) Fz(36px) Mb(-4px) D(ib)')]"
                );

            HtmlNode? finalNode = nodeCollection.FirstOrDefault();

            if (finalNode == null)
            {
                _logger.LogInformation("YahooBtcPriceScraper could not find final HTML element with price");

                return 0;
            }
            else
            {
                string stringPrice = finalNode.InnerText;

                decimal price = Convert.ToDecimal(stringPrice);

                return price;
            }
        }
    }
}
