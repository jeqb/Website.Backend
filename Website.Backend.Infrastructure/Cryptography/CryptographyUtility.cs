using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Website.Backend.Infrastructure.Cryptography
{
    public class CryptographyUtility : ICryptographyUtility
    {
        private readonly string _jwtKey;

        private readonly string _jwtIssuer;

        public CryptographyUtility(string key, string issuer)
        {
            _jwtKey = key;

            _jwtIssuer = issuer;
        }

        /// <summary>
        /// Given an Email address, use the cryptographic key and the issuer to
        /// create a JWT.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GenerateJsonWebToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_jwtIssuer,
                _jwtIssuer,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Given a Password and a Salt, hash them to compare with what's in 
        /// the database.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string HashCredentials(string password, string salt)
        {
            string combined = string.Concat(password, salt);

            byte[] combinedBytes = Encoding.UTF8.GetBytes(combined);

            MD5 md5 = MD5.Create();
            byte[] hashedBytes = md5.ComputeHash(combinedBytes);

            string hashedPasswordAndSalt = Convert.ToBase64String(hashedBytes);

            return hashedPasswordAndSalt;
        }
    }
}
