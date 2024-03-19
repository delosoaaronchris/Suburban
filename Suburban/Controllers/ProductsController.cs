using Suburban.Data;
using Suburban.Models;
using Suburban.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Suburban.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            db = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Brand = product.Brand,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                ExistingImage = product.ProductImage
            };

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Product product = new Product
                {
                    Brand = model.Brand,
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Description = model.Description,
                    ProductImage = uniqueFileName
                };

                db.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products.FindAsync(id);
            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Brand = product.Brand,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                ExistingImage = product.ProductImage
            };

            if (product == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await db.Products.FindAsync(model.Id);
                product.Brand = model.Brand;
                product.Name = model.Name;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.Description = model.Description;

                if (model.ProductPicture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    product.ProductImage = ProcessUploadedFile(model);
                }
                db.Update(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Brand = product.Brand,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                ExistingImage = product.ProductImage
            };
            if (product == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await db.Products.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", product.ProductImage);
            db.Products.Remove(product);
            if (await db.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return db.Products.Any(e => e.Id == id);
        }

        private string ProcessUploadedFile(ProductViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProductPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductPicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}