using decwebapispotify.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<ISpotifyAccountServices, SpotifyAccountServices>(c =>
{
    c.BaseAddress = new Uri("https://accounts.spotify.com/api/token");
});
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ISpotifyService,SpotifyServices>(c =>
{
    c.BaseAddress = new Uri("https://api.spotify.com/v1/");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//app.Run(async (context) =>
//{
 //   context.Response.Redirect("http://your-public-ip:5000");
//});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
