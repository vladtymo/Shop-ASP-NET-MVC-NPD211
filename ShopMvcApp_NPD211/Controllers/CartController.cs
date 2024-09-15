using AutoMapper;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_NPD211.Models;

namespace ShopMvcApp_NPD211.Controllers
{
    public class CartController(ShopMvcDbContext context, IMapper mapper) : Controller
    {
        private readonly ShopMvcDbContext context = context;
        private readonly IMapper mapper = mapper;

        public IActionResult Index()
        {
            //var products = context.Products.Include(x => x.Category).ToList();
            return View(/*mapper.Map<IEnumerable<ProductModel>>(products)*/);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Remove()
        {
            return View();
        }

        public IActionResult Clear()
        {
            return View();
        }
    }
}
