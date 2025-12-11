using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                
                if (context.Products.Any())
                {
                    return;
                }
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Patek Philippe Nautilus 5711",
                        Price = 135000.00M,
                        Category = "Luxury Sport",
                        Stock = 1
                    },
                    new Product
                    {
                        Name = "Rolex Submariner Date",
                        Price = 16500.00M,
                        Category = "Diver Watch",
                        Stock = 3
                    },
                    new Product
                    {
                        Name = "Audemars Piguet Royal Oak",
                        Price = 45000.00M,
                        Category = "Luxury Mechanical",
                        Stock = 2
                    },
                    new Product
                    {
                        Name = "Vacheron Constantin Overseas",
                        Price = 32000.00M,
                        Category = "Haute Horlogerie",
                        Stock = 1
                    },
                    new Product
                    {
                        Name = "A. Lange & Söhne Lange 1",
                        Price = 48000.00M,
                        Category = "Dress Watch",
                        Stock = 2
                    }
                );

                context.SaveChanges();
            }
        }
    }
}