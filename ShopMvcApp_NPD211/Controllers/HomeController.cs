using AutoMapper;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_NPD211.Models;
using System.Diagnostics;

namespace ShopMvcApp_NPD211.Controllers
{
    public class HomeController(IMapper mapper, ShopMvcDbContext context) : Controller
    {
        private readonly IMapper mapper = mapper;
        private readonly ShopMvcDbContext context = context;

        public IActionResult Index()
        {
            // ... working with db ...
            var products = context.Products.Include(x => x.Category).ToList();

            // ~/Home/Index.cshtml
            return View(mapper.Map<IEnumerable<ProductModel>>(products)); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
