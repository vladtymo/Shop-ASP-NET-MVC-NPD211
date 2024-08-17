using Microsoft.AspNetCore.Mvc;
using ShopMvcApp_NPD211.Models;
using System.Diagnostics;

namespace ShopMvcApp_NPD211.Controllers
{
    public class HomeController : Controller
    {
        private string[] colors = { "Red", "Green", "Brown", "Purple" };
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            // ... working with db ...
            // ... logic

            return View(colors); // ~/Home/Index.cshtml
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
