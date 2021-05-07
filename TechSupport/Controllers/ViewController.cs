using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSupport.Data_Access_Layer;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class ViewController : Controller
    {
        private readonly CommonContext _commonContext;
        public ViewController(CommonContext commonContext)
        {
            _commonContext = commonContext;
        }

        public IActionResult Index()
        {
            return View("Pages/Index.cshtml");
        }

        [Authorize("UserPolicy", Roles = "User")]
        public IActionResult UserPrivatePage()
        {
            var tasks = _commonContext.Tasks.ToList();
            return View("Pages/UserPrivatePage.cshtml", tasks);
        }

        public IActionResult LogIn()
        {
            return View("Pages/LogIn.cshtml");
        }

        [HttpPost]
        public ActionResult LogIn(LoginData loginData)
        {
            var user = _commonContext.Users.Where(x => x.Login == loginData.Login).Include("Role").First();

            if (user.Password == loginData.Password)
            {
                var identity = new ClaimsIdentity("aspnetcoreauth", user.Login, user.Role.Name);

                HttpContext.User.AddIdentity(identity);

                return View("Pages/Index.cshtml");
            }
            else
            {
                return View("Pages/Error.cshtml");
            }
        }

        public IActionResult Registration()
        {
            return View("Pages/Registration.cshtml");
        }
        public IActionResult CreateTask()
        {
            return View("Pages/CreateTask.cshtml");
        }
        public IActionResult ExecutorPrivatePage()
        {
            return View("Pages/ExecutorPrivatePage.cshtml");
        }
    }
}