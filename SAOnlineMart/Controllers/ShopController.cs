using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Data;
using System.Linq;

namespace SAOnlineMart.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve all products from the database
            var products = _context.Products.ToList();
            return View(products); // Pass the products to the view
        }
    }
}
