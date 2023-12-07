using decwebapispotify.Models;

namespace decwebapispotify.Services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<Release>> GetNewRelease(string countryCode, int limit, string accessToken);
        
    }
}
