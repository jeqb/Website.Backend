namespace Website.Backend.Infrastructure.Finance
{
    public interface IStockMarketService
    {
        public Task<double> GetSp500PriceChangePercent();
    }
}
