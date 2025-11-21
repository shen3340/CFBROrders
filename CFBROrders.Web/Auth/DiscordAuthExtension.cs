using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace CFBROrders.Web.Auth
{
    public static class DiscordAuthExtension
    {
        public static AuthenticationBuilder AddDiscordAuth(this AuthenticationBuilder builder, IConfiguration config)
        {
            builder.AddOAuth("Discord", options =>
            {
                var discordAuth = config.GetSection("Authentication:Discord");
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
                            new AuthenticationHeaderValue("Bearer", context.AccessToken);

                        var response = await context.Backchannel.SendAsync(request);
                        response.EnsureSuccessStatusCode();

                        var json = await response.Content.ReadAsStringAsync();
                        using var user = JsonDocument.Parse(json);
                        context.RunClaimActions(user.RootElement);
                    },
                    OnRemoteFailure = context =>
                    {
                        context.Response.Redirect("/autherror?error=discord-auth-failed");
                        context.HandleResponse();
                        return Task.CompletedTask;
                    }
                };
            });

            return builder;
        }
    }
}
