using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Data;
using OnlineShop.DATA; 
using System;
using System.Linq;

namespace OnlineShop.DATA 
{
    public static class SeedData 
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                
            }
        }
    }
}