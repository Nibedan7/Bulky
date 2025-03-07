using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _db.Products.ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
           //ViewBag.CategoryList = CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Product = new Product()
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(productVM.Product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _db.Categories.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
            return View(productVM);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _db.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Product Updated Succesfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _db.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Product obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Products.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Product Deleted Succesfully";
            return RedirectToAction("Index");

        }

    }
}
