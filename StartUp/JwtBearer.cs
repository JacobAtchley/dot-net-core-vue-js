using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Jatchley.Samples.StartUp
{
    public static class JwtBearer
    {
        public static void Configure(IApplicationBuilder app, IConfigurationRoot configuration)
        {
            var clientId = configuration["AzureAD:ClientId"];

var tvps = new TokenValidationParameters
        {
                ValidAudience = clientId,

                ValidateIssuer = false,
        };

        // var options = new OAuthBearerAuthenticationOptions
        // {
        //         AccessTokenFormat = new JwtFormat(tvps, new OpenIdConnectCachingSecurityTokenProvider("https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration")),
        // };
        //     app.UseOAuthAuthentication(options);

        }
    }
}