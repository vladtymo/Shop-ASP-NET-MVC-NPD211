using AutoMapper;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_NPD211.Extensions;
using ShopMvcApp_NPD211.Models;

namespace ShopMvcApp_NPD211.Controllers
{
    public class CartController(ShopMvcDbContext context, IMapper mapper) : Controller
    {
        private readonly ShopMvcDbContext context = context;
        private readonly IMapper mapper = mapper;

        public IActionResult Index()
        {
            var ids = HttpContext.Session.Get<List<int>>("cartItems") ?? [];

            var products = context.Products.Where(x => ids.Contains(x.Id)).ToList();
            return View(mapper.Map<IEnumerable<ProductModel>>(products));
        }

        public IActionResult Add(int id)
        {
            var ids = HttpContext.Session.Get<List<int>>("cartItems") ?? [];
            ids.Add(id);

            HttpContext.Session.Set("cartItems", ids);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove(int id)
        {
            // TODO
            return View();
        }

        public IActionResult Clear()
        {
            // TODO
            return View();
        }
    }
}
