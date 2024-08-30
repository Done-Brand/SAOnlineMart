using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAOnlineMart.Data;

namespace SAOnlineMart.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Route("Order/_PendingOrders")]
        public IActionResult PendingOrders()
        {
            var pendingOrders = _context.Orders.ToList();
            return PartialView("_PendingOrders", pendingOrders);
        }


        [HttpPost]
        public IActionResult ShipOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = "Shipped";
                _context.SaveChanges();
            }

            return RedirectToAction("PendingOrders");
     
       }


        public IActionResult Test()
        {
            return Content("Test successful");
        }

    }


}
