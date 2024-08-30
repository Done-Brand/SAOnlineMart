using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using SAOnlineMart.Data;
using SAOnlineMart.Models.DataModels;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;


namespace SAOnlineMart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult AddProductForm()
        {
            return PartialView("_AddProductForm");
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,ImageUrl")] Product product, IFormFile fileUpload)
        {
            if (fileUpload != null && fileUpload.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName);
                string localPath = Path.Combine(@"C:\Users\admin\Documents\GitHub\SAOnlineMart\SAOnlineMart\wwwroot\images\products\", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                using (var fileStream = new FileStream(localPath, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(fileStream);
                }
                product.ImageUrl = "/images/products/" + fileName;
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdminDashboard");
        }

        public IActionResult EditProducts()
        {
            var products = _context.Products.ToList();
            return PartialView("_EditProducts", products);
        }

        public IActionResult EditProductForm(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return PartialView("_EditProductForm", product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product model, IFormFile ProductImage)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Products.Find(model.ProductId);
                if (product == null)
                {
                    return NotFound();
                }

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;

                if (ProductImage != null)
                {
                    var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProductImage.FileName);
                    var filePath = Path.Combine(uploadsDir, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProductImage.CopyTo(stream);
                    }
                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    product.ImageUrl = "/images/" + fileName;
                }
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult RemoveProducts()
        {
            var products = _context.Products.ToList();
            return PartialView("_RemoveProducts", products);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "AdminDashboard");
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return PartialView("_ProductList", products);
        }

    }
}
