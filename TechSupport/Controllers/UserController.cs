using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechSupport.Data_Access_Layer;

namespace TechSupport.Controllers
{
    public class UserController : Controller
    {
        private readonly CommonContext _commonContext;

        public UserController(CommonContext commonContext)
        {
            _commonContext = commonContext;
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult UserPrivatePage()
        {
            var user = HttpContext.User;
            var tasks = _commonContext.Tasks.ToList();
            return View("Pages/UserPrivatePage.cshtml", tasks);
        }

        public IActionResult CreateTask()
        {
            return View("Pages/CreateTask.cshtml");
        }
    }
}