using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CFBROrders.Web.Endpoints
{
    public static class LogoutEndpoints
    {
        public static void MapLogout(this WebApplication app)
        {
            app.MapGet("/logout", async ctx =>
            {
                var logger = NLog.LogManager.GetCurrentClassLogger();

                var username = ctx.User?.FindFirst("Username")?.Value
                               ?? ctx.User?.Identity?.Name
                               ?? "UnknownUser";

                logger.Info($"User {username} logged out.");

                await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                ctx.Response.Redirect("/login");
            });
        }
    }
}
