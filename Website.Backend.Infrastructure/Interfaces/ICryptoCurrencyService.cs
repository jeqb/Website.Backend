namespace Website.Backend.Infrastructure.Interfaces
{
    public interface ICryptoCurrencyService
    {
        public Task<decimal> GetBtcPriceInUsd();
    }
}
