using System.Text.Json.Serialization;

namespace Website.Backend.Infrastructure.Models
{
    /// <summary>
    /// This is the payload coming back from Coin Market Cap for the BTC price query.
    /// Just keeping all the other models in here because they have no use elsewhere and
    /// no point in muddying up the folders.
    /// </summary>
    public class CoinMarketCapBtcResponseModel
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; } = new();
    }

    public class Data
    {
        [JsonPropertyName("1")]
        public OneProperty oneProperty { get; set; } = new();
    }

    /// <summary>
    /// the value is actually "1" in the json response. no idea why.
    /// </summary>
    public class OneProperty
    {
        [JsonPropertyName("quote")]
        public Quote Quote { get; set; }
    }

    public class Quote
    {
        [JsonPropertyName("USD")]
        public Usd Usd { get; set; }
    }

    public class Usd
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}
