using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Models
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}