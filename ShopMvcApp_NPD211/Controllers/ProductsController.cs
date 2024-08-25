using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
