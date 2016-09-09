using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace docker_web_test.StartUp
{
    public static class OpenIdConnect
    {
        public static void ConfigureApp(IApplicationBuilder app, IConfigurationRoot configuration)
        {
           app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                ClientId = configuration["AzureAD:ClientId"],
                Authority = String.Format(configuration["AzureAd:AadInstance"], configuration["AzureAd:Tenant"]),
                ResponseType = OpenIdConnectResponseType.IdToken,
                PostLogoutRedirectUri = configuration["AzureAd:PostLogoutRedirectUri"],
                Events = new OpenIdConnectEvents
                {
                    OnRemoteFailure = OnAuthenticationFailed,
                }
            });
        }

        private static Task OnAuthenticationFailed(FailureContext context)
        {
            context.HandleResponse();
            context.Response.Redirect("/Home/Error?message=" + context.Failure.Message);
            return Task.FromResult(0);
        }
    }    
}