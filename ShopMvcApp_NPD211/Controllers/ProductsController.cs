using AutoMapper;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using ShopMvcApp_NPD211.Extensions;

namespace ShopMvcApp_NPD211.Controllers
{
    public class ProductsController(
        IMapper mapper, 
        ShopMvcDbContext context, 
        IFilesService filesService) : Controller
    {
        private readonly ShopMvcDbContext context = context;
        private readonly IFilesService filesService = filesService;
        private readonly IMapper mapper = mapper;

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
        public async Task<IActionResult> Create(CreateProductModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories(); 
                return View(model);
            }

            // 1 - manual mapping
            //var entity = new Product()
            //{
            //    Name = model.Name,
            //    Price = model.Price,
            //    Discount = model.Discount,
            //    Quantity = model.Quantity,
            //    CategoryId = model.CategoryId,
            //    Description = model.Description,
            //    ImageUrl = model.ImageUrl,
            //};

            // 2 - auto mapping
            var entity = mapper.Map<Product>(model);

            // save file to server
            if (model.Image != null)
                entity.ImageUrl = await filesService.SaveProductImage(model.Image);

            context.Products.Add(entity);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.ADMIN)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            if (product == null) return NotFound(); // 404

            LoadCategories();
            return View(product);
        }

        [Authorize(Roles = Roles.ADMIN)]
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

        [Authorize(Roles = Roles.ADMIN)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = context.Products.Find(id);

            if (product == null) return NotFound(); // 404

            if (product.ImageUrl != null)
                await filesService.DeleteProductImage(product.ImageUrl);

            context.Products.Remove(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
