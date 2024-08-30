using Microsoft.AspNetCore.Mvc;
using SAOnlineMart.Data;
using SAOnlineMart.Models.DataModels;
using System.Threading.Tasks;

public class AdminUserController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminUserController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: AdminUser/Create
    public IActionResult Create()
    {
        return View();
    }

   
    // POST: AdminUser/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminUser adminUser)
    {
        if (_context.AdminUsers.Any(u => u.Email == adminUser.Email))
        {
            ModelState.AddModelError("Email", "User with this email already exists.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(adminUser);
            await _context.SaveChangesAsync();
            // Redirect to the login page after successful registration
            return RedirectToAction("Login", "AdminUser");
        }
        return View(adminUser);
    }


    // GET: AdminUser/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: AdminUser/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(AdminUser adminUser)
    {
        // Check if the user exists in the database by email
        var user = _context.AdminUsers.FirstOrDefault(u => u.Email == adminUser.Email);

        if (user == null)
        {
            ModelState.AddModelError("Email", "User does not exist.");
        }
        else if (user.Password != adminUser.Password)
        {
            ModelState.AddModelError("Password", "Incorrect password.");
        }
        else
        {
            return RedirectToAction("Index", "AdminDashboard");
        }
        return View(adminUser);
    }




    public IActionResult GetAdminUsers()
    {
        try
        {
            var adminUsers = _context.AdminUsers
                                     .Select(u => new AdminUser
                                     {
                                         Username = u.Username,
                                         Email = u.Email
                                     })
                                     .ToList();

            return PartialView("_AdminUserTable", adminUsers);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }








}
