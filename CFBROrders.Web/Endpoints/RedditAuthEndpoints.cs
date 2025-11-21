using CFBROrders.SDK.Interfaces.Services;
using CFBROrders.SDK.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CFBROrders.Web.Endpoints
{
    public static class RedditAuthEndpoints
    {
        public static void MapRedditAuth(this WebApplication app)
        {
            app.MapGet("/auth-reddit", async ctx =>
            {
                var returnUrl = ctx.Request.Query["ReturnUrl"].FirstOrDefault() ?? "/";
                await ctx.ChallengeAsync("Reddit", new AuthenticationProperties { RedirectUri = "/signin-reddit?ReturnUrl=" + returnUrl });
            });

            app.MapGet("/signin-reddit", async (HttpContext ctx, IUserService UserService, ITeamService TeamService) =>
            {
                var logger = NLog.LogManager.GetCurrentClassLogger();

                var result = await ctx.AuthenticateAsync("Reddit");

                // if Oauth failed completely
                if (!result.Succeeded || result.Principal == null)
                {
                    logger.Error("Reddit OAuth failed: no principal or authentication did not succeed.");

                    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    ctx.Response.Redirect("/autherror?error=reddit-auth-failed");
                    return;
                }

                var redditId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                var username = result.Principal.FindFirstValue(ClaimTypes.Name);

                // if there's no reddit id
                if (redditId == null)
                {
                    logger.Error($"Reddit OAuth failed: missing Reddit ID for username: {username}.");

                    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    ctx.Response.Redirect("/autherror?error=missingid");
                    return;
                }

                // Reddit IDs are strings (not numeric), but log in the same style anyway
                if (string.IsNullOrWhiteSpace(redditId))
                {
                    logger.Error($"Reddit OAuth failed: invalid Reddit ID format: {redditId} for username: {username}.");

                    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    ctx.Response.Redirect("/autherror?error=invalid_reddit_id");
                    return;
                }

                var user = UserService.GetUserByPlatformAndUsername("reddit", username);

                // if the user hasn't already made a CFBR account
                if (user == null)
                {
                    logger.Error($"Reddit OAuth failed: User doesn't have an existing CFBR account with redditId: {redditId} and username {username}");

                    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    ctx.Response.Redirect("/autherror?error=not-registered");
                    return;
                }

                // verify that user is on an active team
                if (user.CurrentTeam == -1)
                {
                    logger.Error($"Reddit OAuth failed: Username {username} isn't on an active team");

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

                logger.Info($"User {username} successfully logged in via Reddit.");

                await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                ctx.Response.Redirect("/");
            });
        }
    }
}
