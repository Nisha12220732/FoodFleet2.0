using Microsoft.EntityFrameworkCore;
using RestaurantService.Models;
namespace RestaurantService.Data
{
    public class RestaurantDbcontext : DbContext
    {
        public RestaurantDbcontext(DbContextOptions<RestaurantDbcontext> options): base(options)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; } 
        
    }
}
