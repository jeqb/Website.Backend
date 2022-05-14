namespace Website.Backend.Infrastructure.Finance
{
    public interface ICryptoCurrencyService
    {
        public Task<decimal> GetBtcPriceInUsd();
    }
}
