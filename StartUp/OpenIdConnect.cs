using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Jatchley.Samples.StartUp
{
    public static class OpenIdConnect
    {
        public static void ConfigureApp(IApplicationBuilder app, IConfigurationRoot configuration, ILogger logger)
        {
            var options = new OpenIdConnectOptions
            {
                ClientId = configuration["AzureAD:ClientId"],
                Authority = String.Format(configuration["AzureAd:AadInstance"], configuration["AzureAd:Tenant"]),
                ResponseType = OpenIdConnectResponseType.IdToken,
                PostLogoutRedirectUri = configuration["AzureAd:PostLogoutRedirectUri"],
                Events = new OpenIdConnectEvents
                {
                    OnRemoteFailure = OnAuthenticationFailed,
                    OnTokenValidated = x =>
                    {
                        var token = x.SecurityToken.RawPayload;
                        logger.LogInformation(token);
                        logger.LogInformation($"Issuer : {x.SecurityToken.Issuer}");
                        logger.LogInformation($"Audiences : {x.SecurityToken.Audiences.Aggregate(new StringBuilder(), (b,i) => b.Append(i).Append(""))}");
                        var tokenClaim = new Claim("token", token);

                        var identity = x.Ticket.Principal.Identity;

                        var claimIdentity = new ClaimsIdentity(
                            identity.AuthenticationType,
                            "name",
                            "role");

                        claimIdentity.AddClaim(tokenClaim);
                        claimIdentity.AddClaims(x.Ticket.Principal.Claims);

                        var princiapl = new ClaimsPrincipal(claimIdentity);

                        x.Ticket = new AuthenticationTicket(
                            princiapl,
                            x.Ticket.Properties,
                            x.Ticket.AuthenticationScheme);

                        return Task.CompletedTask;
                    }

                },

            };

            options.TokenValidationParameters.ValidateIssuer = false;

            app.UseOpenIdConnectAuthentication(options);
        }

        private static Task OnAuthenticationFailed(FailureContext context)
        {
            context.HandleResponse();
            context.Response.Redirect("/Home/Error?message=" + context.Failure.Message);
            return Task.FromResult(0);
        }
    }
}