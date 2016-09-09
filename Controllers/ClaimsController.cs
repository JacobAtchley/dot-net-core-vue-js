using docker_web_test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace docker_web_test.Controllers
{
    [Authorize]
    public class ClaimsController : Controller 
    {
        public IActionResult Index()
        {
            return View(new ClaimViewModel(this.User));
        }
    }
}