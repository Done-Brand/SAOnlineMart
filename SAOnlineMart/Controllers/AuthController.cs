using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Data;
using SAOnlineMart.Models.DataModels;
using SAOnlineMart.Models.ViewModels;
using System.Linq;

namespace SAOnlineMart.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginModel.InputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            // Set the username in ViewBag
            ViewBag.UserName = user.FirstName + " " + user.LastName;

            return RedirectToAction("Index", "Home", new { userName = ViewBag.UserName });
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterModel.InputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError(string.Empty, "Email is already registered.");
                return View(model);
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password 
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            ViewBag.UserName = user.FirstName + " " + user.LastName;

            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Profile()
        {
            // Ensure the user is logged in
            if (ViewBag.UserName == null)
            {
                return RedirectToAction("Login");
            }

            // Display profile information
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
