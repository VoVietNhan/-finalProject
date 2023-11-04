using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Data
{
    public class OrderContext: DbContext
    {
        public OrderContext() { }
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>(entity =>
            {
                entity.HasMany(c => c.OrderDetails)
                      .WithOne()
                      .HasForeignKey(cd => cd.OrderId);
                entity.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
            });
            builder.Entity<OrderDetails>(entity =>
            {
                entity.HasOne(cd => cd.Order)
                      .WithMany(c => c.OrderDetails)
                      .HasForeignKey(cd => cd.OrderId);
            });
        }
    }
}
