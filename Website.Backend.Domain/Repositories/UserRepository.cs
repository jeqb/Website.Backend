using Website.Backend.Domain.Repositories.Interfaces;

namespace Website.Backend.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Private Properties

        private readonly List<UserDomain> _users = new List<UserDomain>
        {
            new UserDomain
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

        public async Task<IEnumerable<UserDomain>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDomain> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDomain> Create(UserDomain entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDomain> Update(UserDomain entity)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDomain> GetUserByEmail(string email)
        {
            Task.Yield();

            return new UserDomain
            {
                EmailAddress = email,
                CreatedDate = DateTime.Now,
            };
        }
    }
}
