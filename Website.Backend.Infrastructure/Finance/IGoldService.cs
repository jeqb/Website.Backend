namespace Website.Backend.Infrastructure.Finance
{
    public interface IGoldService
    {
        public Task<decimal> GetSpotPriceInUsd();
    }
}
