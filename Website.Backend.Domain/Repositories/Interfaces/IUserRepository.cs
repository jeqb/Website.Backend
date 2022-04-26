namespace Website.Backend.Domain.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User?> GetUserByEmail(string email);
    }
}
