using Jatchley.Samples.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jatchley.Samples.Controllers
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