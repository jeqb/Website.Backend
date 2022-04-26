namespace Website.Backend.Infrastructure.Interfaces
{
    public interface ICredentialsHasher
    {
        public string HashCredentials(string password, string salt);
    }
}
