using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DeliverySystem.Models
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public DbSet<ActiveCourierDelivery> ActiveCourierDeliveries { get; set; }
        
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveCourierDelivery>().HasKey(t => new {t.CourierId, t.Id} );
            modelBuilder.Entity<ActiveCourierDelivery>().HasOne(c => c.DeliveryOrder)
                .WithOne(c => c.ActiveCourierDelivery).OnDelete(DeleteBehavior.Restrict);
        }
    }
}