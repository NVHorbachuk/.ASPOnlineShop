using Microsoft.AspNetCore.Http;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItem> GetCart()
        {
            return _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        }

        public void AddToCart(int productId)
        {
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                List<CartItem> cart = GetCart();
                CartItem item = cart.FirstOrDefault(c => c.ProductId == productId);

                int currentQuantityInCart = item != null ? item.Quantity : 0;

                if (currentQuantityInCart + 1 <= product.Stock)
                {
                    if (item != null)
                    {
                        item.Quantity++;
                    }
                    else
                    {
                        cart.Add(new CartItem { ProductId = productId, Quantity = 1 });
                    }

                    SaveCart(cart);
                }
            }
        }

        public void RemoveFromCart(int productId)
        {
            List<CartItem> cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
        }

        public void Clear()
        {
            _httpContextAccessor.HttpContext.Session.Remove("Cart");
        }

        private void SaveCart(List<CartItem> cart)
        {
            _httpContextAccessor.HttpContext.Session.SetObjectAsJson("Cart", cart);
        }
    }
}