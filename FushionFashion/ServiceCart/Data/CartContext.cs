using BusinessObject.Entities.Cart;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata;

namespace ServiceCart.Data
{
    public class CartContext:DbContext      
    {
        public CartContext(DbContextOptions options) : base(options)
        {
        }

        public CartContext()
        {
        }
        public DbSet<Cart> Carts { get;set; }
        public DbSet<CartDetail> CartDetails { get;set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasMany(c => c.CartDetails)
                      .WithOne()
                      .HasForeignKey(cd => cd.CartId);
            });

            modelBuilder.Entity<CartDetail>(entity =>
            {
                entity.HasOne(cd => cd.Cart)
                      .WithMany(c => c.CartDetails)
                      .HasForeignKey(cd => cd.CartId);
            });
        }
    }
}
