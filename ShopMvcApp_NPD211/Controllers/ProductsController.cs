using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_NPD211.Models;

namespace ShopMvcApp_NPD211.Controllers
{
    public class ProductsController : Controller
    {
        private ShopMvcDbContext context = new();
        public IActionResult Index()
        {
            // load products from db
            var products = context.Products.Include(x => x.Category).ToList();

            return View(products);
        }

        private void LoadCategories()
        {
            // Transfer data from controller to View
            // -1- using model (only 1 object)
            // -2- using ViewData["Key"]
            // -3- using ViewBag.Key

            ViewBag.Categories = new SelectList(context.Categories.ToList(), "Id", "Name");
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProductModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories(); 
                return View(model);
            }

            var entity = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Discount = model.Discount,
                Quantity = model.Quantity,
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };

            context.Products.Add(entity);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            if (product == null) return NotFound(); // 404

            LoadCategories();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(model);
            }

            context.Products.Update(model);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);

            if (product == null) return NotFound(); // 404

            context.Products.Remove(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
