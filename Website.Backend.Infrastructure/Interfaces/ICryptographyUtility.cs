namespace Website.Backend.Infrastructure.Interfaces
{
    public interface ICryptographyUtility
    {
        public string HashCredentials(string password, string salt);

        public string GenerateJsonWebToken(string email);
    }
}
