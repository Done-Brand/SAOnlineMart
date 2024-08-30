using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Data;
using SAOnlineMart.Models.DataModels;
using System.Linq;
using System.Threading.Tasks;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    // View Cart
    public IActionResult Index()
    {
        var cartItems = _context.CartItems.ToList();
        return View(cartItems);
    }

    // Add to Cart
    public async Task<IActionResult> AddToCart(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return NotFound();
        }

        var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId);
        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            };
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity++;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // Remove from Cart
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem == null)
        {
            return NotFound();
        }

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }


    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int id, int quantity)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem == null)
        {
            return NotFound();
        }

        cartItem.Quantity = quantity;
        await _context.SaveChangesAsync();

        // Calculate the total price for the item and the cart
        var newTotalPrice = cartItem.Price * cartItem.Quantity;
        var cartTotal = _context.CartItems.Sum(item => item.Price * item.Quantity);

        return Json(new { newTotalPrice = newTotalPrice, cartTotal = cartTotal });
    }




    // Checkout Action
    public IActionResult Checkout()
    {
        var cartItems = _context.CartItems.ToList();
        if (!cartItems.Any())
        {
            TempData["Message"] = "Your cart is empty!";
            return RedirectToAction("Index", "Shop");
        }

        return View(cartItems);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(Order model)
    {
        if (ModelState.IsValid)
        {
            // Calculate the total amount based on the items in the cart
            model.TotalAmount = _context.CartItems.Sum(i => i.Price * i.Quantity);
            model.OrderDate = DateTime.Now;

            // Save the order details in the database
            _context.Orders.Add(model);
            await _context.SaveChangesAsync();

            // Clear the cart after the order is placed
            _context.CartItems.RemoveRange(_context.CartItems);
            await _context.SaveChangesAsync();

            // Redirect to the "Thank You" page
            return RedirectToAction("ThankYou");
        }

        // If the model state is invalid, redisplay the checkout form with errors
        return View("Checkout", _context.CartItems.ToList());
    }

    public IActionResult ThankYou()
    {
        return View("~/Views/Shared/ThankYou.cshtml");
    }




    public IActionResult OrderConfirmation()
    {
        return View();
    }


}
