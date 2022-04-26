using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Website.Backend.Domain;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.Extensions;
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

        private readonly ICredentialsHasher _credentialsHasher;

        public LoginService(IConfiguration config, IRepositoryFactory repositoryFactory,
            ICredentialsHasher credentialsHasher)
        {
            _key = config["Jwt:Key"];

            _issuer = config["Jwt:Issuer"];

            _userRepository = repositoryFactory.CreateUserRepository();

            _credentialsHasher = credentialsHasher;
        }

        /// <summary>
        /// TODO: rework this. It needs to verify password hashes and whatnot.
        /// Just using this code now to keep moving
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<UserModel?> AuthenticateUserCredentials(LoginCredentialsModel loginCredentials)
        {
            User user = await _userRepository.GetUserByEmail(loginCredentials.EmailAddress);

            if (user == null)
            {
                // no user found
                return null;
            }
            else
            {
                string providedPassword = loginCredentials.Password;
                string retrievedSalt = user.Salt;

                string hashToCheck = _credentialsHasher.HashCredentials(providedPassword, retrievedSalt);

                if (hashToCheck == user.PasswordHash)
                {
                    // valid credentials provided
                    return user.ToModel();
                }
                else
                {
                    // invalid credentials provided
                    return null;
                }
            }
        }

        public async Task<string> GenerateJsonWebToken(UserModel userInfo)
        {
            await Task.Yield();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
                new Claim("DateOfJoining", userInfo.CreatedDate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_issuer,
                _issuer,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
