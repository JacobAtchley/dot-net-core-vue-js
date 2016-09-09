using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Jatchley.Samples.StartUp
{
    public static class OpenIdConnect
    {
        public static void ConfigureApp(IApplicationBuilder app, IConfigurationRoot configuration)
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