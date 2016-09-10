using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Jatchley.Samples.Controllers
{
    public class AccountController : Controller
    {
        private ILogger<AccountController> Logger { get; set; }

        public AccountController(ILogger<AccountController> logger)
        {
            Logger = logger;
        }
        // GET: /Account/Login
        [HttpGet]
        public async Task Login()
        {
            if (HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated)
                await HttpContext.Authentication.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
        }

        // POST: /Account/LogOff
        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            Logger.LogInformation("Logging off");

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //await HttpContext.Authentication.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
                await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return this.LocalRedirect("/");
        }

        [HttpGet]
        public async Task EndSession()
        {
            // If AAD sends a single sign-out message to the app, end the user's session, but don't redirect to AAD for sign out.
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
