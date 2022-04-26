using System.Security.Cryptography;
using System.Text;
using Website.Backend.Infrastructure.Interfaces;

namespace Website.Backend.Infrastructure
{
    public class CredentialsHasher : ICredentialsHasher
    {
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
