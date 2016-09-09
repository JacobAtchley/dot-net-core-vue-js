using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jatchley.Samples.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        public ProductsController()
        {
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}