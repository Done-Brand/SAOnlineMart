using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Models;
using System.Diagnostics;

namespace SAOnlineMart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string userName)
        {
            ViewBag.UserName = userName;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Contact
        public IActionResult Contact()
        {
            return View();
        }

        // POST: SubmitContactForm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContactForm(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {

                TempData["SuccessMessage"] = "Thank you for reaching out to us. We will get back to you as soon as possible.";
                return RedirectToAction("Contact");
            }

            return View("Contact", model);
        }
    }
}
