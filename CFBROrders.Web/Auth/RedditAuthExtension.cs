using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;

namespace CFBROrders.Web.Auth
{
    public static class RedditAuthExtension
    {
        public static AuthenticationBuilder AddRedditAuth(this AuthenticationBuilder builder, IConfiguration config)
        {
            builder.AddOAuth("Reddit", options =>
             {
                 var redditAuth = config.GetSection("Authentication:Reddit");
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
                     OnRemoteFailure = context =>
                     {
                         // If users click cancel within reddit Oauth2
                         context.Response.Redirect("/autherror?error=reddit-auth-failed");
                         context.HandleResponse();
                         return Task.CompletedTask;
                     },

                     OnRedirectToAuthorizationEndpoint = context =>
                     {
                         context.Response.Redirect(context.RedirectUri + "&duration=temporary");
                         return Task.CompletedTask;
                     }
                 };
             });

            return builder;
        }
    }
}
