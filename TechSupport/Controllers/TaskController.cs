using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechSupport.Data_Access_Layer;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly CommonContext _commonContext;

        public TaskController(CommonContext commonContext)
        {
            _commonContext = commonContext;
        }

        public object Task()
        {
            var items = _commonContext.Tasks.ToList();
            return items;
        }
    }
}