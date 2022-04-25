namespace Website.Backend.Models
{
    public class FinancialInformationModel
    {
        // Price per ounce
        public decimal GoldSpotPrice { get; set; }

        public decimal BitcoinPrice { get; set; }

        public double Sp500PriceChangePercent { get; set; }
    }
}
