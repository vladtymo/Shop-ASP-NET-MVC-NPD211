using Core.Services;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ShopMvcApp_NPD211.Controllers
{
    [Authorize]
    public class OrdersController(ShopMvcDbContext ctx, ICartService cartService) : Controller
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
            var order = new Order()
            {
                CreationDate = DateTime.Now,
                UserId = CurrentUserId!,
                Products = cartService.GetProducts().ToList()
            };

            ctx.Orders.Add(order);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
