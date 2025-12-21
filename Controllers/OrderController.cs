using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using OnlineShop.Data;
using OnlineShop.DATA;               
using OnlineShop.Helpers;
using OnlineShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");

            if (cart == null || cart.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            if (ModelState.IsValid)
            {
                foreach (var item in cart)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);

                    if (product != null)
                    {
                        if (product.Stock < item.Quantity)
                        {
                            ModelState.AddModelError("", $"Вибачте, товару '{product.Name}' залишилося лише {product.Stock} шт.");
                            return View(order);
                        }
                        var orderItem = new OrderItem
                        {
                            ProductId = product.Id,
                            Quantity = item.Quantity,
                            Price = product.Price
                        };
                        order.OrderItems.Add(orderItem);

                        product.Stock -= item.Quantity;
                        _context.Update(product);
                    }
                }

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                HttpContext.Session.Remove("Cart");

                return RedirectToAction("Completed");
            }

            return View(order);
        }

        public IActionResult Completed()
        {
            return View();
        }
    }
}