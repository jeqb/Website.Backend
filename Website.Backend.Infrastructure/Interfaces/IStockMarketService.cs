namespace Website.Backend.Infrastructure.Interfaces
{
    public interface IStockMarketService
    {
        public Task<double> GetSp500PriceChangePercent();
    }
}
