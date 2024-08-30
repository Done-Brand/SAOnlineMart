using Microsoft.AspNetCore.Mvc;

namespace SAOnlineMart.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
