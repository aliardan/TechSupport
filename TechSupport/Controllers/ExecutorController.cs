using System.Linq;
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
            var executor = HttpContext.User;
            var tasks = _commonContext.Tasks.ToList();
            return View("Pages/ExecutorPrivatePage.cshtml", tasks);
        }

        [Authorize(Roles = "Executor, Admin")]
        public IActionResult ExecutorPrivatePageForm(int id)
        {
            var task = _commonContext.Tasks.Where(x => x.Id == id).First();
            return View("Pages/ExecutorPrivatePageForm.cshtml", task);
        }
    }
}