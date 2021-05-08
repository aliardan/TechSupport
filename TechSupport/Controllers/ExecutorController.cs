using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechSupport.Data_Access_Layer;

namespace TechSupport.Controllers
{
    public class ExecutorController : Controller
    {
        private readonly CommonContext _commonContext;

        public ExecutorController(CommonContext commonContext)
        {
            _commonContext = commonContext;
        }
        
        [Authorize(Roles = "Executor, Admin")]
        public IActionResult ExecutorPrivatePage()
        {
            return View("Pages/ExecutorPrivatePage.cshtml");
        }
    }
}