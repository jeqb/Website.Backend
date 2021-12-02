using Website.Backend.Models;

namespace Website.Backend.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserByEmail(string email);
    }
}
