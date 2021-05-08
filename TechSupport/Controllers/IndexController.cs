using Microsoft.AspNetCore.Mvc;

namespace TechSupport.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View("Pages/Index.cshtml");
        }
    }
}