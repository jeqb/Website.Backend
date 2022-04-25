using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Services
{
    public class FinancialService : IFinancialService
    {
        public FinancialService()
        {

        }

        public async Task<FinancialInformationModel> GetFinancialInformationAsync()
        {
            await Task.Yield();

            return new FinancialInformationModel
            {
                // reconsider decimal. maybe just use double.
                BitcoinPrice = (decimal)40000.45,
                GoldSpotPrice = (decimal)2000.00,
            };
        }
    }
}
