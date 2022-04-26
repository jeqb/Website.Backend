using Website.Backend.Models;

namespace Website.Backend.Services.Interfaces
{
    public interface ILoginService
    {
        public Task<UserModel?> AuthenticateUserCredentials(LoginCredentialsModel loginCredentials);

        public Task<string> GenerateJsonWebToken(UserModel userInfo);
    }
}
