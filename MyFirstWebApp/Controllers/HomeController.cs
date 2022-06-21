using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFirstWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddShops ()
        {

            List<ShopModel> shops = new List<ShopModel>();
            using (var db = new DemoContext())
            {
                shops = db.Shops.ToList();
            }
            ViewBag.Shops = shops;

                return View();
        }
        [HttpPost]
        public IActionResult AddShops(ShopModel shop)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DemoContext())
                {
                    db.Add(shop);
                    db.SaveChanges();
                }

            }
                            
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
