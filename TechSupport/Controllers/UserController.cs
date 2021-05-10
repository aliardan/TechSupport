using System;
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
            var tasks = _commonContext.Tasks.ToList();
            return View("Pages/UserPrivatePage.cshtml", tasks);
        }

        [Authorize(Roles = "Customer, Admin")]
        [HttpGet()]
        public IActionResult UserPrivatePageForm(int id)
        {
            var task = _commonContext.Tasks.Where(x => x.Id == id).First();
            return View("Pages/UserPrivatePageForm.cshtml", task);
        }

        [Authorize(Roles = "Customer, Admin")]
        public IActionResult CreateTask()
        {
            return View("Pages/CreateTask.cshtml");
        }

        [Authorize(Roles = "Customer, Admin")]
        [HttpPost]
        public IActionResult CreateTask(CreateTaskData createTaskData)
        {
            Random i = new Random();

            var existingTask = _commonContext
                .Tasks
                .FirstOrDefault(x => x.Name == createTaskData.Name);

            if (existingTask != null)
            {
                return View("Pages/CreateTask.cshtml");
            }

            var executorIds = _commonContext.Users.Where(x => x.Role.Name == "Executor").Select(x=>x.Id).ToList();
            var executorIndex = i.Next(executorIds.Count - 1);
            var executorId = executorIds[executorIndex];

            var newTask = new Task
            {
                Name = createTaskData.Name,
                Id = createTaskData.Id,
                Description = createTaskData.Description,
                CreatorId = createTaskData.CreatorId,
                ExecutorId = executorId,
                Status = i.Next(2),
                Priority = i.Next(3),
                CreatedDateTime = DateTime.Now,
                ClosedDateTime = DateTime.Now.AddDays(i.Next(1, 5))
            };
            _commonContext.Tasks.Add(newTask);
            _commonContext.SaveChanges();
            
            return View("Pages/Index.cshtml");
        }
    }
}