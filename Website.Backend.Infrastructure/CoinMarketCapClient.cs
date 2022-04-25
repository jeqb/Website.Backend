using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Website.Backend.Infrastructure.Interfaces;
using Website.Backend.Infrastructure.Models;

namespace Website.Backend.Infrastructure
{
    public class CoinMarketCapClient : ICryptoCurrencyService
    {
        private const string _baseUrl = "https://pro-api.coinmarketcap.com";

        private readonly ILogger<CoinMarketCapClient> _logger;

        private readonly HttpClient _httpClient;

        private readonly string _apiKey;

        public CoinMarketCapClient(ILogger<CoinMarketCapClient> logger, HttpClient httpClient,
            string apiKey)
        {
            _logger = logger;

            _httpClient = httpClient;

            _apiKey = apiKey;
        }

        public async Task<decimal> GetBtcPriceInUsd()
        {
            // build request
            string url = _baseUrl + "/v2/cryptocurrency/quotes/latest?slug=bitcoin";

            UriBuilder uriBuilder = new UriBuilder(url);

            HttpRequestMessage requestMessage = new();
            requestMessage.RequestUri = uriBuilder.Uri;
            requestMessage.Method = HttpMethod.Get;
            requestMessage.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);


            // deal with response
            HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);

            if (responseMessage.IsSuccessStatusCode != true)
            {
                HttpStatusCode responseCode = responseMessage.StatusCode;

                _logger.LogInformation("CoinMarketCapClient failed to price with response: {responseCode}", responseCode);

                return 0;
            }
            else
            {
                string rawResponse = await responseMessage.Content.ReadAsStringAsync();

                CoinMarketCapBtcResponseModel? jsonResponse =
                    JsonSerializer.Deserialize<CoinMarketCapBtcResponseModel>(rawResponse);

                if (jsonResponse == null)
                {
                    _logger.LogInformation("CoinMarketCapClient could not deserialize response JSON payload");

                    return 0;
                }
                else
                {
                    decimal price = jsonResponse.Data.oneProperty.Quote.Usd.Price;

                    return price;
                }
            }
        }
    }
}
