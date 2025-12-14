using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using System.Linq;

namespace OnlineShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var product = _context.Products.FirstOrDefault(m => m.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home"); 
            }
            return View(product);
        }

        
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return View(product);
        }

        
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }
    }
}