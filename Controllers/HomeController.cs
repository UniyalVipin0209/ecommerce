using Beml.ECommerce.App.Data;
using Beml.ECommerce.App.Models;
using Beml.ECommerce.App.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Beml.ECommerce.App.Utility;
using Microsoft.AspNetCore.Http;

namespace Beml.ECommerce.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Products.Include(u => u.Category).Include(u => u.ApplicationType),
                Categories = _db.Categories,
                ApplicationTypes = _db.ApplicationTypes
            };
            return View(homeVM);
        }

        public IActionResult Details(int? id)
        {
            DetailsVM detailVM = new DetailsVM()
            {
                Product = _db.Products.Include(u => u.Category).Include(u => u.ApplicationType).Where(u => u.ProductId == id).FirstOrDefault(),
                ExistInCart = false
            };
            return View(detailVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id)
        {
            List<ShoppingCart> liShoppingCart= new List<ShoppingCart>();
           
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null
               && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                liShoppingCart = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }
            liShoppingCart.Add(new ShoppingCart { ProductId = id });
            HttpContext.Session.Set(WC.SessionCart, liShoppingCart.Cast<byte>().ToList());
          
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
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