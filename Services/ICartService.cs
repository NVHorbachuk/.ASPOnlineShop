using OnlineShop.Models;
using System.Collections.Generic;

namespace OnlineShop.Services
{
    public interface ICartService
    {
        List<CartItem> GetCart();
        void AddToCart(int productId);
        void RemoveFromCart(int productId);
        void Clear();
    }
}