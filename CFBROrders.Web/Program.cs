using CFBROrders.SDK.DataModel;
using CFBROrders.SDK.Interfaces;
using CFBROrders.SDK.Interfaces.Services;
using CFBROrders.SDK.Models;
using CFBROrders.SDK.Repositories;
using CFBROrders.SDK.Services;
using CFBROrders.Web.Areas.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Radzen;
using System.Security.Claims;
using System.Text.Json;

var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CFBROrdersConnectionString")
    ?? throw new InvalidOperationException("Connection string 'CFBROrdersConnectionString' not found.");

builder.Services.AddRazorPages();
builder.Services.AddRadzenComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    })
    .AddOAuth("Discord", options =>
    {
        var discordAuth = builder.Configuration.GetSection("Authentication:Discord");
        options.ClientId = discordAuth["ClientId"]!;
        options.ClientSecret = discordAuth["ClientSecret"]!;
        options.CallbackPath = discordAuth["CallbackPath"];

        options.AuthorizationEndpoint = "https://discord.com/api/oauth2/authorize";
        options.TokenEndpoint = "https://discord.com/api/oauth2/token";
        options.UserInformationEndpoint = "https://discord.com/api/users/@me";
        options.Scope.Add("identify");
        options.SaveTokens = true;

        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");

        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
                var response = await context.Backchannel.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using var user = System.Text.Json.JsonDocument.Parse(json);
                context.RunClaimActions(user.RootElement);
            }
        };
    })
    .AddOAuth("Reddit", options =>
    {
        var redditAuth = builder.Configuration.GetSection("Authentication:Reddit");
        options.ClientId = redditAuth["ClientId"]!;
        options.ClientSecret = redditAuth["ClientSecret"]!;
        options.CallbackPath = redditAuth["CallbackPath"];

        options.AuthorizationEndpoint = "https://www.reddit.com/api/v1/authorize";
        options.TokenEndpoint = "https://www.reddit.com/api/v1/access_token";
        options.UserInformationEndpoint = "https://oauth.reddit.com/api/v1/me";
        options.Scope.Add("identity");
        options.SaveTokens = true;

        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");

        var credentials = Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes($"{redditAuth["ClientId"]}:{redditAuth["ClientSecret"]}"));

        var handler = new HttpClientHandler();
        var httpClient = new HttpClient(handler);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "CFBROrders/0.1 by Disastrous_Rush4196");
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

        options.Backchannel = httpClient;

        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
                request.Headers.Add("User-Agent", "CFBROrders/0.1 by Disastrous_Rush4196");

                var response = await context.Backchannel.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using var user = System.Text.Json.JsonDocument.Parse(json);
                context.RunClaimActions(user.RootElement);
            },

            OnRedirectToAuthorizationEndpoint = context =>
            {
                context.Response.Redirect(context.RedirectUri + "&duration=temporary");
                return Task.CompletedTask;
            }
        };
    });

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
builder.Host.UseNLog();

CFBROrdersDatabaseFactory.Setup(connectionString);
builder.Services.AddScoped<IUnitOfWork, NPocoUnitOfWork>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITerritoriesService, TerritoriesService>();
builder.Services.AddScoped<ITeamsService, TeamsService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/auth-discord", async (HttpContext ctx) =>
{
    var returnUrl = ctx.Request.Query["ReturnUrl"].FirstOrDefault() ?? "/";
    await ctx.ChallengeAsync("Discord", new AuthenticationProperties { RedirectUri = "/signin-discord?ReturnUrl=" + returnUrl });
});

app.MapGet("/signin-discord", async (HttpContext ctx, ApplicationDBContext db) =>
{
    var result = await ctx.AuthenticateAsync("Discord");
    if (!result.Succeeded || result.Principal == null)
    {
        ctx.Response.Redirect("/login?error=discord_auth_failed");
        return;
    }

    var discordId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
    var username = result.Principal.FindFirstValue(ClaimTypes.Name);

    if (discordId == null)
    {
        ctx.Response.Redirect("/login?error=missingid");
        return;
    }

    if (!long.TryParse(discordId, out var discordLong))
    {
        ctx.Response.Redirect("/login?error=invalid_discord_id");
        return;
    }

    var user = await db.Users.FirstOrDefaultAsync(u =>
        u.Platform == "discord" && (u.Uname == username || u.Uname == username + "$0"));
    if (user == null)
    {
        ctx.Response.Redirect("/login?error=not_registered");
        return;
    }

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Uname ?? ""),
        new Claim("Platform", user.Platform ?? "")
    };

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    ctx.Response.Redirect("/");
});

app.MapGet("/auth-reddit", async (HttpContext ctx) =>
{
    var returnUrl = ctx.Request.Query["ReturnUrl"].FirstOrDefault() ?? "/";
    await ctx.ChallengeAsync("Reddit", new AuthenticationProperties { RedirectUri = "/signin-reddit?ReturnUrl=" + returnUrl });
});

app.MapGet("/signin-reddit", async (HttpContext ctx, ApplicationDBContext db) =>
{
    var result = await ctx.AuthenticateAsync("Reddit");
    if (!result.Succeeded || result.Principal == null)
    {
        ctx.Response.Redirect("/login?error=reddit_auth_failed");
        return;
    }

    var redditId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
    var username = result.Principal.FindFirstValue(ClaimTypes.Name);

    if (redditId == null)
    {
        ctx.Response.Redirect("/login?error=missingid");
        return;
    }

    var user = await db.Users.FirstOrDefaultAsync(u => u.Platform == "reddit" && u.Uname == username);
    if (user == null)
    {
        ctx.Response.Redirect("/login?error=not_registered");
        return;
    }

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Uname ?? ""),
        new Claim("Platform", user.Platform ?? "")
    };

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    ctx.Response.Redirect("/");
});

app.MapGet("/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    ctx.Response.Redirect("/login");
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
