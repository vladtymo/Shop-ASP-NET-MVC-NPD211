using Microsoft.AspNetCore.Mvc;
using Core.Services;

namespace ShopMvcApp_NPD211.Controllers
{
    public class CartController(ICartService cartService) : Controller
    {
        public IActionResult Index()
        {
            return View(cartService.GetProductDtos());
        }
            
        public IActionResult Add(int id)
        {
            cartService.Add(id);
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
