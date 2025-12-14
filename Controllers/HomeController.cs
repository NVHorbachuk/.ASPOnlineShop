using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using OnlineShop.Data;
using OnlineShop.Models;
using System.Diagnostics;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

      
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index(string searchString)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            return View(products.ToList());
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