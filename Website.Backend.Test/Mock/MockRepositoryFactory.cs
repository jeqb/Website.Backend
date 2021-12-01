using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Backend.Models;
using Website.Backend.Repositories;

namespace Website.Backend.Test.Mock
{
    internal class MockRepositoryFactory : IRepositoryFactory
    {
        public IRepository<Message> CreateMessageRepository()
        {
            throw new NotImplementedException();
        }

        public IRepository<User> CreateUserRepository()
        {
            throw new NotImplementedException();
        }
    }
}
