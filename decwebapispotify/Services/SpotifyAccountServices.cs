using decwebapispotify.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace decwebapispotify.Services
{
    public class SpotifyAccountServices : ISpotifyAccountServices
    {
        private readonly HttpClient _httpClient;
        public SpotifyAccountServices(HttpClient httpClient)
        {
                _httpClient = httpClient;
        }
        public async Task<string> GetToken(string ClientId, string ClientSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "token");
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}")));
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type" ,"client_credentials"}
            });  //request object 
        

            var reponse = await _httpClient.SendAsync(request);
            var responseContent = await reponse.Content.ReadAsStringAsync();
            Console.WriteLine($"API 回應內容：{responseContent}");
            reponse.EnsureSuccessStatusCode();
            using var responseStream = await reponse.Content.ReadAsStreamAsync();
            var authResult = await JsonSerializer.DeserializeAsync<AuthResult>(responseStream);

            return authResult.access_token;

        }
    }
}
