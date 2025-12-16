using CFBROrders.SDK.Data_Models;
using CFBROrders.SDK.DataModel;
using CFBROrders.SDK.Interfaces;
using CFBROrders.SDK.Interfaces.Services;
using CFBROrders.SDK.Repositories;
using CFBROrders.SDK.Services;
using CFBROrders.Web.Auth;
using CFBROrders.Web.Data;
using CFBROrders.Web.Endpoints;
using CFBROrders.Web.Handlers;
using CFBROrders.Web.Helpers;
using CFBROrders.Web.Interfaces.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>(optional: true);

var connectionString = builder.Configuration.GetConnectionString("CFBROrdersConnectionString")
    ?? throw new InvalidOperationException("Connection string 'CFBROrdersConnectionString' not found.");

builder.Services.AddRazorPages();
builder.Services.AddRadzenComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(connectionString));

// Add authentication, I'm assuming Keycloak will be added here later
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/autherror";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    })
    .AddDiscordAuth(builder.Configuration)
    .AddRedditAuth(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
builder.Host.UseNLog();

CFBROrdersDatabaseFactory.Setup(connectionString);

builder.Services.AddScoped<IOperationResult, DBOperationResult>();
builder.Services.AddScoped<IUnitOfWork, NPocoUnitOfWork>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITerritoryService, TerritoryService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IOrderAllocationService, OrderAllocationService>();
builder.Services.AddScoped<IUserOrderService, UserOrderService>();
builder.Services.AddScoped<ITurnInfoService, TurnInfoService>();

builder.Services.AddSingleton<IColorHandler, ColorHandler>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

var logger = NLog.LogManager.GetCurrentClassLogger();
logger.Debug("init main");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<UserLoggingMiddleware>();
app.UseAuthorization();

// External API Calls 
app.MapDiscordAuth();
app.MapRedditAuth();
app.MapLogout();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();