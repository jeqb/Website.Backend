namespace Website.Backend.Infrastructure.Cryptography
{
    public interface ICryptographyUtility
    {
        public string HashCredentials(string password, string salt);

        public string GenerateJsonWebToken(string email);
    }
}
