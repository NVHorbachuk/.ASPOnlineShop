using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.DATA;      
using OnlineShop.Helpers;    
using OnlineShop.Models;     
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            return View(cart);
        }

        public async Task<IActionResult> Add(int id)
        {
            Product product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

                CartItem item = cart.FirstOrDefault(c => c.Product.Id == id);

                if (item != null)
                {
                    item.Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = product, Quantity = 1 });
                }

                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            if (cart != null)
            {
                var item = cart.FirstOrDefault(c => c.Product.Id == id);
                if (item != null)
                {
                    cart.Remove(item);
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }
            return RedirectToAction("Index");
        }
    }
}