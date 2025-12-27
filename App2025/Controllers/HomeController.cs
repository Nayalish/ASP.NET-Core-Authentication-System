using App2025.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace App2025.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly App2025Context _context;

        public HomeController(ILogger<HomeController> logger, App2025Context context)
        {
            _logger = logger;
            _context = context;
        }
        
        public IActionResult Login()
        {
            //user is a builtin property provided by asp.net core
            //Identity represent who the user is (name,authentication type login status) comes from authentication system.
            //Identity.IsAuthenticated check either the user is login or not

            // If already logged in, go directly to Home page
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                TempData["success"] = "Welcome";
                return RedirectToAction("Index");
                
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminUser admn)
        {
            var user = _context.AdminUsers
                .FirstOrDefault(m => m.Name == admn.Name && m.Password == admn.Password);
          
            //if user is not login 
            if (user != null)
            {
                //claim contain user information, identity tell authentication status,claimprinciple tell login user 
                //cookie shows remember login(store data in browser) while sesssion store data temporary(in server/app)
                var claims = new List<Claim> {             //Create a list to store user information.
            new Claim(ClaimTypes.Name, user.Name),
            // optionally roles
        };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties { IsPersistent = true /* optional */ });
                TempData["success"] = "Login successful. Welcome, Admin!";

                return RedirectToAction("Index");

            }
            TempData["error"] = "Invalid credentials";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        //Signup
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Signup(AdminUser model)
        {
            if (ModelState.IsValid)
            {
                TempData["success"] = "Signup successfully done!";
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Login));
            }
          
            return View();
        }

        [Authorize]
        public IActionResult Index(AdminUser model)
        {
            return View(model);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
