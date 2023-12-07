using decwebapispotify.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace decwebapispotify.Services
{
    public class SpotifyServices : ISpotifyService
    {
        public readonly HttpClient _httpClient;
        public SpotifyServices(HttpClient httpClient) 
        { 
            _httpClient = httpClient;
        }

       public async Task<IEnumerable<Release>> GetNewRelease(string countryCode, int limit, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"browse/new-releases?country={countryCode}&limit={limit}");
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetNewReleaseResult>(responseStream);
           
            return responseObject?.albums?.items.Select(i => new Release
            {
                Name = i.name,
                Date = i.release_date,
                ImageUrl = i.images.FirstOrDefault().url,
                Link = i.external_urls.spotify,
                Artists = string.Join(",",i.artists.Select(i =>i.name))
            });
        }
    }
}
