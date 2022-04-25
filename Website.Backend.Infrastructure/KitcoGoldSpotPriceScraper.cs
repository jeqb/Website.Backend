using HtmlAgilityPack;
using Website.Backend.Infrastructure.Interfaces;

namespace Website.Backend.Infrastructure
{
    public class KitcoGoldSpotPriceScraper : IGoldService
    {
        private const string _kitcoUrl = "https://www.kitco.com/charts/livegold.html";

        private readonly HttpClient _httpClient;

        public KitcoGoldSpotPriceScraper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetSpotPriceInUsd()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_kitcoUrl);

            string htmlFile = await response.Content.ReadAsStringAsync();

            decimal price = ParseHtml(htmlFile);

            return price;
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
