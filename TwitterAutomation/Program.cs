using TwitterDiscovery.Procurement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();

// Twitter client registration
builder.Services.AddSingleton<TwitterClient>(sp =>
{
    var bearerToken =Environment.GetEnvironmentVariable("TWITTER_BEARER_TOKEN") ?? builder.Configuration["Twitter:BearerToken"] ?? throw new Exception("Twitter bearer token not configured");

    return new TwitterClient(bearerToken);
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

//for railway rendering page port is needed
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    app.Urls.Add($"http://0.0.0.0:{port}");
}

app.Run();
