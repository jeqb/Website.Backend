using Website.Backend.Domain.Repositories.Interfaces;

namespace Website.Backend.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Private Properties

        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                EmailAddress = "bob@foo.com",

                CreatedDate = DateTime.Now,
            }
        };

        #endregion

        // TODO: inject database connection here.
        public UserRepository()
        {

        }

        public async Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Create(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            Task.Yield();

            return new User
            {
                EmailAddress = email,
                CreatedDate = DateTime.Now,
            };
        }
    }
}
