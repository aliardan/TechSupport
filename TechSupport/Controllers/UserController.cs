using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechSupport.Data_Access_Layer;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class UserController : Controller
    {
        private readonly CommonContext _commonContext;

        public UserController(CommonContext commonContext)
        {
            _commonContext = commonContext;
        }

        [Authorize(Roles = "Customer, Admin")]
        public IActionResult UserPrivatePage()
        {
            var user = HttpContext.User;
            var tasks = _commonContext.Tasks.ToList();
            return View("Pages/UserPrivatePage.cshtml", tasks);
        }

        [Authorize(Roles = "Customer, Admin")]
        public IActionResult UserPrivatePageForm()
        {
            var user = HttpContext.User;
            var tasks = _commonContext.Tasks.ToList();
            return View("Pages/UserPrivatePageForm.cshtml", tasks);
        }

        public IActionResult CreateTask()
        {
            return View("Pages/CreateTask.cshtml");
        }

        [Authorize(Roles = "Customer, Admin")]
        [HttpPost]
        public IActionResult CreateTask(CreateTaskData createTaskData)
        {
            var existingTask = _commonContext
                .Tasks
                .FirstOrDefault(x => x.Name == createTaskData.Name);

            if (existingTask != null)
            {
                return View("Pages/CreateTask.cshtml");
            }

            var newTask = new Task
            {
                Name = createTaskData.Name,
                Description = createTaskData.Description
            };
            _commonContext.Tasks.Add(newTask);
            _commonContext.SaveChanges();
            
            return View("Pages/Index.cshtml");
        }
    }
}