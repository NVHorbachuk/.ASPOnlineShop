using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Data;
using OnlineShop.Models; 
using System;
using System.Linq;

namespace OnlineShop.DATA
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                if (context.Products.Any())
                {
                    return;   
                }

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Casio G-Shock",
                        Category = "Спортивні",
                        Description = "Надміцний годинник для активного відпочинку",
                        Price = 4500,
                        Stock = 15,
                        ImageUrl = "https://example.com/gshock.jpg" 
                    },
                    new Product
                    {
                        Name = "Tissot Gentleman",
                        Category = "Класичні",
                        Description = "Швейцарська якість та елегантний стиль",
                        Price = 18000,
                        Stock = 8
                    },
                    new Product
                    {
                        Name = "Apple Watch Series 9",
                        Category = "Смарт-годинники",
                        Description = "Ваш розумний помічник на кожен день",
                        Price = 19999,
                        Stock = 10
                    },
                    new Product
                    {
                        Name = "Garmin Fenix 7",
                        Category = "Спортивні",
                        Description = "Професійний мультиспортивний годинник",
                        Price = 32000,
                        Stock = 5
                    }
                );

                // 3. Зберігаємо зміни в базу
                context.SaveChanges();
            }
        }
    }
}