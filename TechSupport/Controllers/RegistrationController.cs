using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSupport.Data_Access_Layer;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly CommonContext _commonContext;

        public RegistrationController(CommonContext commonContext)
        {
            _commonContext = commonContext;
        }

        public IActionResult Registration()
        {
            return View("Pages/Registration.cshtml");
        }

        [HttpPost]
        public IActionResult Registration(RegistrationData registrationData)
        {
            var existingUser = _commonContext
                .Users
                .FirstOrDefault(x => x.Login == registrationData.Login);

            if (existingUser != null)
            {
                return View("Pages/Registration.cshtml");
            }

            var userRole = _commonContext.Roles.First(x => x.Name == "Customer");
            var newUser = new User
            {
                Login = registrationData.Login,
                Password = registrationData.Password,
                Role = userRole
            };
            _commonContext.Users.Add(newUser);
            _commonContext.SaveChanges();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, newUser.Login),
                new Claim(ClaimTypes.Role, newUser.Role.Name)

            };

            var identity = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimTypes.Name,
                ClaimTypes.Role);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            HttpContext.User = new ClaimsPrincipal(identity);
            return View("Pages/Index.cshtml");
        }
    }
}