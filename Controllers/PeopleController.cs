using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jatchley.Samples.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }        
    }
}