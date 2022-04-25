using Website.Backend.Infrastructure.Interfaces;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Services
{
    public class FinancialService : IFinancialService
    {
        private readonly ICryptoCurrencyService _cryptoCurrencyService;

        private readonly IGoldService _goldService;

        private readonly IStockMarketService _stockMarketService;

        public FinancialService(ICryptoCurrencyService cryptoCurrencyService, IGoldService goldService,
            IStockMarketService stockMarketService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;

            _goldService = goldService;

            _stockMarketService = stockMarketService;
        }

        public async Task<FinancialInformationModel> GetFinancialInformationAsync()
        {
            Task<decimal> bitcoinTask = _cryptoCurrencyService.GetBtcPriceInUsd();
            Task<decimal> goldTask = _goldService.GetSpotPriceInUsd();
            Task<double> sp500Task = _stockMarketService.GetSp500PriceChangePercent();

            await Task.WhenAll(bitcoinTask, goldTask, sp500Task);

            decimal bitcoinPrice = bitcoinTask.Result;
            decimal goldPrice = goldTask.Result;
            double sp500PriceChangePercent = sp500Task.Result;

            return new FinancialInformationModel
            {
                // reconsider decimal. maybe just use double.
                BitcoinPrice = bitcoinPrice,
                GoldSpotPrice = goldPrice,
                Sp500PriceChangePercent = sp500PriceChangePercent
            };
        }
    }
}
