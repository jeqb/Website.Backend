using Website.Backend.Models;

namespace Website.Backend.Services.Interfaces
{
    public interface ILoginService
    {
        public Task<string?> AuthenticateUserCredentials(LoginCredentialsModel loginCredentials);
    }
}
