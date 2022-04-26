using Website.Backend.Domain;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.Infrastructure.Interfaces;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Services
{
    public class LoginService : ILoginService
    {
        private readonly string _key;

        private readonly string _issuer;

        private readonly IUserRepository _userRepository;

        private readonly ICryptographyUtility _cryptoUtil;

        public LoginService(IConfiguration config, IRepositoryFactory repositoryFactory,
            ICryptographyUtility cryptoUtil)
        {
            _key = config["Jwt:Key"];

            _issuer = config["Jwt:Issuer"];

            _userRepository = repositoryFactory.CreateUserRepository();

            _cryptoUtil = cryptoUtil;
        }

        /// <summary>
        /// TODO: rework this. It needs to verify password hashes and whatnot.
        /// Just using this code now to keep moving
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string?> AuthenticateUserCredentials(LoginCredentialsModel loginCredentials)
        {
            User? user = await _userRepository.GetUserByEmail(loginCredentials.EmailAddress);

            if (user == null)
            {
                // no user found
                return null;
            }
            else
            {
                string providedPassword = loginCredentials.Password;
                string retrievedSalt = user.Salt;

                string hashToCheck = _cryptoUtil.HashCredentials(providedPassword, retrievedSalt);

                if (hashToCheck == user.PasswordHash)
                {
                    // valid credentials provided
                    string jwt = _cryptoUtil.GenerateJsonWebToken(user.EmailAddress);

                    return jwt;
                }
                else
                {
                    // invalid credentials provided
                    return null;
                }
            }
        }
    }
}
