using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_NPD211.Extensions;
using System.Security.Claims;

namespace ShopMvcApp_NPD211.Controllers
{
    [Authorize]
    public class OrdersController(ShopMvcDbContext ctx) : Controller
    {
        private string? CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public IActionResult Index()
        {
            var orders = ctx.Orders
                            .Include(x => x.Products)
                            .Where(x => x.UserId == CurrentUserId)
                            .ToList();
            return View(orders);
        }   

        public IActionResult Add()
        {
            var ids = HttpContext.Session.Get<List<int>>("cartItems") ?? [];

            var products = ctx.Products.Where(x => ids.Contains(x.Id)).ToList();

            var order = new Order()
            {
                CreationDate = DateTime.Now,
                UserId = CurrentUserId!,
                Products = products
            };

            ctx.Orders.Add(order);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
