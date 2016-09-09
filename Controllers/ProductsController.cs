using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace docker_web_test.Controllers
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