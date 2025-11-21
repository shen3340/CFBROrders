using CFBROrders.SDK.Interfaces.Services;
using CFBROrders.SDK.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CFBROrders.Web.Endpoints
{
    public static class DiscordAuthEndpoints
    {
        public static void MapDiscordAuth(this WebApplication app)
        {
            app.MapGet("/auth-discord", async ctx =>
            {
                var returnUrl = ctx.Request.Query["ReturnUrl"].FirstOrDefault() ?? "/";
                await ctx.ChallengeAsync("Discord", new AuthenticationProperties { RedirectUri = "/signin-discord?ReturnUrl=" + returnUrl });
            });

            app.MapGet("/signin-discord", async (HttpContext ctx, IUserService UserService, ITeamService TeamService) =>
            {
                var logger = NLog.LogManager.GetCurrentClassLogger();

                var result = await ctx.AuthenticateAsync("Discord");

                // if Oauth failed completely
                if (!result.Succeeded || result.Principal == null)
                {
                    logger.Error("Discord OAuth failed: no principal or authentication did not succeed.");

                    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    ctx.Response.Redirect("/autherror?error=discord-auth-failed");
                    return;
                }

                var discordId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                var username = result.Principal.FindFirstValue(ClaimTypes.Name);

                // if there's no discord id
                if (discordId == null)
                {
                    logger.Error($"Discord OAuth failed: missing Discord ID for username: {username}.");

                    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    ctx.Response.Redirect("/autherror?error=invalid_discord_id");
                    return;
                }

                // if discord id changed how the ID is formatted
                if (!long.TryParse(discordId, out var discordLong))
                {
                    logger.Error($"Discord OAuth failed: invalid Discord ID format: {discordId} for username: {username}.");

                    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    ctx.Response.Redirect("/autherror?error=invalid-discord-id");
                    return;
                }

                var user = UserService.GetUserByPlatformAndUsername("discord", username);

                // if user hasn't already made a CFBR account
                if (user == null)
                {
                    logger.Error($"Discord OAuth failed: User doesn't have an existing CFBR account with discordId: {discordId} and username {username}");

                    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    ctx.Response.Redirect("/autherror?error=not-registered");
                    return;
                }

                // verify that user is on an active team
                if (user.CurrentTeam == -1)
                {
                    logger.Error($"Discord OAuth failed: Username {username} isn't on an active team");

                    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    ctx.Response.Redirect("/autherror?error=team-not-active");
                    return;
                }

                var claims = new List<Claim>
                {
                    new ("UserId", user.Id.ToString()),
                    new ("Username", user.Uname ?? ""),
                    new ("Platform", user.Platform ?? ""),
                    new ("CurrentTeam", TeamService.GetTeamNameByTeamId(user.CurrentTeam)),
                    new ("Overall", UserService.GetOverallByUserId(user.Id).ToString()),
                    new ("Color", TeamService.GetTeamColorByTeamId(user.CurrentTeam))

                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                logger.Info($"User {username} successfully logged in via Discord.");

                await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                ctx.Response.Redirect("/");
            });
        }
    }
}
