using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Backend.Models;
using Website.Backend.Repositories;

namespace Website.Backend.Test.Mock
{
    internal class MockUserRepository : IRepository<User>
    {
        private readonly List<User> _mockUserData;

        public MockUserRepository(List<User> mockUserData)
        {
            _mockUserData = mockUserData;
        }

        public Task<User> Create(User entity)
        {
            // calculate id
            int id = _mockUserData.Count() + 1;

            entity.Id = id;

            _mockUserData.Add(entity);

            return Task.FromResult(entity);
        }

        public Task Delete(int id)
        {
            User itemToRemove = _mockUserData
                                        .Select(x => x)
                                        .Where(x => x.Id == id)
                                        .FirstOrDefault();

            if (itemToRemove != null)
            {
                _mockUserData.Remove(itemToRemove);
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            await Task.Yield();

            IEnumerable<User> result = _mockUserData;

            return result;
        }

        public Task<User> GetById(int id)
        {
            User result = _mockUserData
                            .Select(x => x)
                            .Where(x => x.Id == id)
                            .FirstOrDefault();

            return Task.FromResult(result);
        }

        public Task<User> Update(User entity)
        {
            User foundUser = _mockUserData.First(x => x.Id == entity.Id);

            if (foundUser != null)
            {
                if (!string.IsNullOrWhiteSpace(entity.Name)) foundUser.Name = entity.Name;
                if (!string.IsNullOrWhiteSpace(entity.EmailAddress)) foundUser.EmailAddress = entity.EmailAddress;
                if (!string.IsNullOrWhiteSpace(entity.PasswordHash)) foundUser.PasswordHash = entity.PasswordHash;
                foundUser.UpdatedDate = DateTimeOffset.Now;

                return Task.FromResult(foundUser);
            }
            else
            {
                throw new Exception("User not found!");
            }
        }
    }
}
