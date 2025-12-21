using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Services;
using OnlineShop.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly AppDbContext _context;

        public CartController(ICartService cartService, AppDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCart();

            List<CartItemViewModel> cartViewModel = new List<CartItemViewModel>();

            foreach (var item in cartItems)
            {
                var product = _context.Products.Find(item.ProductId);

                if (product != null)
                {
                    cartViewModel.Add(new CartItemViewModel
                    {
                        Product = product,
                        Quantity = item.Quantity
                    });
                }
            }

            return View(cartViewModel);
        }

        [HttpPost]
        public IActionResult Add(int id)
        {
            _cartService.AddToCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            _cartService.Clear();
            return RedirectToAction("Index");
        }
    }
}