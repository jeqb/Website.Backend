namespace Website.Backend.Infrastructure.Interfaces
{
    public interface IGoldService
    {
        public Task<decimal> GetSpotPriceInUsd();
    }
}
