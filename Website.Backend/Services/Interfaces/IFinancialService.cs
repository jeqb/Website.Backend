using Website.Backend.Models;

namespace Website.Backend.Services.Interfaces
{
    public interface IFinancialService
    {
        public Task<FinancialInformationModel> GetFinancialInformationAsync();
    }
}
