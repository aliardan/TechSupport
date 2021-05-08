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
    public class LoginController : Controller
    {
        private readonly CommonContext _commonContext;

        public LoginController(CommonContext commonContext)
        {
            _commonContext = commonContext;
        }
        
        public IActionResult LogIn()
        {
            return View("Pages/LogIn.cshtml");
        }

        [HttpPost]
        public IActionResult LogIn(LoginData loginData)
        {
            var user = _commonContext
                .Users
                .Where(x => x.Login == loginData.Login && x.Password == loginData.Password)
                .Include("Role")
                .FirstOrDefault();
            
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, user.Role.Name)

                };

                var identity = new ClaimsIdentity(
                    claims, 
                    "ApplicationCookie",
                    ClaimTypes.Name,
                    ClaimTypes.Role);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return View("Pages/Index.cshtml");
            }
            else
            {
                return View("Pages/LogIn.cshtml");
            }
        }
    }
}