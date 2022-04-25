using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Website.Backend.Infrastructure.Interfaces;

namespace Website.Backend.Infrastructure
{
    public class KitcoGoldSpotPriceScraper : IGoldService
    {
        private const string _kitcoUrl = "https://www.kitco.com/charts/livegold.html";

        private readonly ILogger<KitcoGoldSpotPriceScraper> _logger;

        private readonly HttpClient _httpClient;

        public KitcoGoldSpotPriceScraper(ILogger<KitcoGoldSpotPriceScraper> logger, HttpClient httpClient)
        {
            _logger = logger;

            _httpClient = httpClient;
        }

        public async Task<decimal> GetSpotPriceInUsd()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_kitcoUrl);

            if (response.IsSuccessStatusCode != true)
            {
                _logger.LogInformation("KitcoGoldSpotPriceScraper could not retrieve gold spot price data.");

                return 0;
            }
            else
            {
                string htmlFile = await response.Content.ReadAsStringAsync();

                decimal price;

                try
                {
                    price = ParseHtml(htmlFile);
                }
                catch (Exception ex)
                {
                    // if this happens, the source HTML probably changed.
                    _logger.LogInformation("KitcoGoldSpotPriceScraper could not parse the returned HTML");

                    price = 0;
                }
                
                return price;
            }
        }

        private decimal ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            string spotPriceString = htmlDoc.GetElementbyId("sp-bid").InnerText;

            decimal price = Convert.ToDecimal(spotPriceString);

            return price;
        }
    }
}
