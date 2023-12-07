using decwebapispotify.Models;
using decwebapispotify.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace decwebapispotify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpotifyAccountServices _spotifyAccountServices;
        private readonly IConfiguration _configuration;
        private readonly ISpotifyService _spotifyService;

        public HomeController(ISpotifyAccountServices spotifyAccountServices,IConfiguration configuration, ISpotifyService spotifyService)
        {
            _spotifyAccountServices = spotifyAccountServices;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> Index()
        {
            var newreleases = await GetReleases();
            return View(newreleases);
        }
        private async Task<IEnumerable<Release>> GetReleases()
        {
            try
            {
        
                var token = await _spotifyAccountServices.GetToken(_configuration["Spotify:ClientId"], _configuration["Spotify:ClientSecret"]);
              
                var newReleases = await _spotifyService.GetNewRelease("GB", 20, token);
                return newReleases;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex is HttpRequestException httpEx)
                {
                    Console.WriteLine($"HttpStatusCode: {httpEx.StatusCode}");
                    //Console.WriteLine($"Response: {httpEx.Response}");
                }
                Debug.WriteLine(ex.ToString());
                return Enumerable.Empty<Release>();
                // Debug.WriteLine(ex.ToString());
                // return Enumerable.Empty<Release>();
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
