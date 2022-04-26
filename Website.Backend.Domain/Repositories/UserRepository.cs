using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.TableStorage;
using Website.Backend.TableStorage.Entities;
using Website.Backend.Domain.Extensions;

namespace Website.Backend.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TableStorageClient _tableStorageClient;

        public UserRepository(TableStorageClient tableStorageClient)
        {
            _tableStorageClient = tableStorageClient;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(Guid id)
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

        public async Task Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            UserEntity? user = await _tableStorageClient.GetUserByEmailAsync(email);

            if (user == null)
            {
                return null;
            }
            else
            {
                return user.ToDomain();
            }
        }
    }
}
