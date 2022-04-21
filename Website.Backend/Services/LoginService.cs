using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Website.Backend.Domain;
using Website.Backend.Domain.Repositories.Interfaces;
using Website.Backend.Extensions;
using Website.Backend.Models;
using Website.Backend.Services.Interfaces;

namespace Website.Backend.Services
{
    public class LoginService : ILoginService
    {
        private readonly string _key;

        private readonly string _issuer;

        private readonly IUserRepository _userRepository;

        public LoginService(string key, string issuer, IRepositoryFactory repositoryFactory)
        {
            _key = key;

            _issuer = issuer;

            _userRepository = repositoryFactory.CreateUserRepository();
        }

        /// <summary>
        /// TODO: rework this. It needs to verify password hashes and whatnot.
        /// Just using this code now to keep moving
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<User> AuthenticateUserCredentials(LoginCredentials loginCredentials)
        {
            UserDomain user = await _userRepository.GetUserByEmail(loginCredentials.EmailAddress);

            if (user == null)
            {
                return new User
                {
                    Id = 0
                };
            }
            else
            {
                return user.ToModel();
            }
        }

        public Task<string> GenerateJsonWebToken(User userInfo)
        {
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

            return Task.FromResult(
                new JwtSecurityTokenHandler().WriteToken(token)
                );
        }
    }
}
