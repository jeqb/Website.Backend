using Website.Backend.Models;

namespace Website.Backend.Services.Interfaces
{
    public interface ILoginService
    {
        public Task<User> AuthenticateUserCredentials(LoginCredentials loginCredentials);

        public Task<string> GenerateJsonWebToken(User userInfo);
    }
}
