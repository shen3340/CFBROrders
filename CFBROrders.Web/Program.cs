using CFBROrders.SDK.DataModel;
using CFBROrders.SDK.Interfaces;
using CFBROrders.SDK.Interfaces.Services;
using CFBROrders.SDK.Repositories;
using CFBROrders.SDK.Services;
using CFBROrders.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using System.Text.Json;
using NLog;
using NLog.Web;
using Radzen;

var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CFBROrdersConnectionString") ?? throw new InvalidOperationException("Connection string 'CFBROrdersConnectionString' not found.");

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = "Discord";
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
        options.Scope.Add("email");

        options.SaveTokens = true;

        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");
        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
        options.ClaimActions.MapJsonKey("urn:discord:avatar", "avatar");

        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);

                var response = await context.Backchannel.SendAsync(request);
                response.EnsureSuccessStatusCode();

                using var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                context.RunClaimActions(user.RootElement);
            }
        };
    })
    .AddOAuth("Reddit", options =>
    {
        var redditAuth = builder.Configuration.GetSection("Authentication:Reddit");
        options.ClientId = redditAuth["ClientId"]!;
        options.ClientSecret = redditAuth["ClientSecret"]!;
        options.CallbackPath = redditAuth["CallbackPath"]; // e.g., "/signin-reddit"

        options.AuthorizationEndpoint = "https://www.reddit.com/api/v1/authorize";
        options.TokenEndpoint = "https://www.reddit.com/api/v1/access_token";
        options.UserInformationEndpoint = "https://oauth.reddit.com/api/v1/me";

        options.Scope.Add("identity");
        options.SaveTokens = true;

        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");

        var httpClient = new HttpClient(new HttpClientHandler());
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{options.ClientId}:{options.ClientSecret}"))
            );
        httpClient.DefaultRequestHeaders.Add("User-Agent", "CFBROrders/0.1 by Disastrous_Rush4196");

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

                using var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                context.RunClaimActions(user.RootElement);
            }
        };
    });

builder.Services.AddRadzenComponents();
builder.Services.AddServerSideBlazor();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = false;
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
builder.Host.UseNLog();

CFBROrdersDatabaseFactory.Setup(connectionString);

//builder.Services.AddScoped<IOperationResult, DBOperationResult>();
builder.Services.AddScoped<IUnitOfWork, NPocoUnitOfWork>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationClaimsPrincipalFactory>();

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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/auth-discord", async (HttpContext ctx) =>
{
    var returnUrl = ctx.Request.Query["ReturnUrl"].FirstOrDefault() ?? "/";
    await ctx.ChallengeAsync("Discord", new AuthenticationProperties
    {
        RedirectUri = returnUrl
    });
});

app.MapGet("/signin-discord", async (HttpContext ctx, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) =>
{
    var result = await ctx.AuthenticateAsync("Discord");
    if (!result.Succeeded || result.Principal == null)
    {
        ctx.Response.Redirect("/login?error=discord");
        return;
    }

    var discordId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
    var username = result.Principal.FindFirstValue(ClaimTypes.Name);
    var email = result.Principal.FindFirstValue(ClaimTypes.Email);

    if (discordId == null)
    {
        ctx.Response.Redirect("/login?error=missingid");
        return;
    }

    var user = await userManager.FindByLoginAsync("Discord", discordId);
    if (user == null && email != null)
    {
        user = await userManager.FindByEmailAsync(email);
    }

    if (user == null)
    {
        user = new ApplicationUser
        {
            UserName = username ?? $"discord_{discordId}",
            Email = email
        };
        await userManager.CreateAsync(user);
        await userManager.AddLoginAsync(user, new UserLoginInfo("Discord", discordId, "Discord"));
    }

    await signInManager.SignInAsync(
    user,
    new AuthenticationProperties { IsPersistent = true },
    IdentityConstants.ApplicationScheme
);


    ctx.Response.Redirect("/");
});

app.MapGet("/auth-reddit", async (HttpContext ctx) =>
{
    var returnUrl = ctx.Request.Query["ReturnUrl"].FirstOrDefault() ?? "/";
    await ctx.ChallengeAsync("Reddit", new AuthenticationProperties
    {
        RedirectUri = returnUrl
    });
});

app.MapGet("/signin-reddit", async (HttpContext ctx, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) =>
{
    var result = await ctx.AuthenticateAsync("Reddit");
    if (!result.Succeeded || result.Principal == null)
    {
        ctx.Response.Redirect("/login?error=reddit");
        return;
    }

    var redditId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
    var username = result.Principal.FindFirstValue(ClaimTypes.Name);

    if (redditId == null)
    {
        ctx.Response.Redirect("/login?error=missingid");
        return;
    }

    var user = await userManager.FindByLoginAsync("Reddit", redditId);
    if (user == null)
    {
        user = new ApplicationUser
        {
            UserName = username ?? $"reddit_{redditId}"
        };
        await userManager.CreateAsync(user);
        await userManager.AddLoginAsync(user, new UserLoginInfo("Reddit", redditId, "Reddit"));
    }

    await signInManager.SignInAsync(
        user,
        new AuthenticationProperties { IsPersistent = true },
        IdentityConstants.ApplicationScheme
    );

    ctx.Response.Redirect("/");
});

app.MapPost("/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync(IdentityConstants.ApplicationScheme);
    ctx.Response.Redirect("/");
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();