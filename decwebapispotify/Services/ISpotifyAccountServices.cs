namespace decwebapispotify.Services
{
    public interface ISpotifyAccountServices
    {
        Task<string> GetToken(string clientId, string clientSercret);
    }
}
