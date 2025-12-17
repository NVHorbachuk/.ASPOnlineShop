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
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

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
                    var orderItem = new OrderItem
                    {
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        Price = item.Product.Price
                    };
                    order.OrderItems.Add(orderItem);

                    var productInDb = await _context.Products.FindAsync(item.Product.Id);
                    if (productInDb != null)
                    {
                        productInDb.Stock -= item.Quantity;
                        _context.Update(productInDb);
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