using Website.Backend.Infrastructure.Interfaces;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Services
{
    public class FinancialService : IFinancialService
    {
        private readonly ICryptoCurrencyService _cryptoCurrencyService;

        private readonly IGoldService _goldService;

        public FinancialService(ICryptoCurrencyService cryptoCurrencyService, IGoldService goldService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;

            _goldService = goldService;
        }

        public async Task<FinancialInformationModel> GetFinancialInformationAsync()
        {
            Task<decimal> bitcoinTask = _cryptoCurrencyService.GetBtcPriceInUsd();
            Task<decimal> goldTask = _goldService.GetSpotPriceInUsd();

            await Task.WhenAll(bitcoinTask, goldTask);

            decimal bitcoinPrice = bitcoinTask.Result;
            decimal goldPrice = goldTask.Result;

            return new FinancialInformationModel
            {
                // reconsider decimal. maybe just use double.
                BitcoinPrice = bitcoinPrice,
                GoldSpotPrice = goldPrice
            };
        }
    }
}
