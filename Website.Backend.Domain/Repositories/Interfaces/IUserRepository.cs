namespace Website.Backend.Domain.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<UserDomain>
    {
        public Task<UserDomain> GetUserByEmail(string email);
    }
}
