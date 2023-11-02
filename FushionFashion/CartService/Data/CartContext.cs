using BusinessObject.Entities.Cart;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace CartService.Data
{
    public class CartContext : DbContext
        
    {
        public CartContext() { }
        public CartContext(DbContextOptions<CartContext> options) : base(options) { }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cart>(entity =>
            {
                entity.HasMany(c => c.CartDetails)
                      .WithOne()
                      .HasForeignKey(cd => cd.CartId);
            });

            builder.Entity<CartDetail>(entity =>
            {
                entity.HasOne(cd => cd.Cart)
                      .WithMany(c => c.CartDetails)
                      .HasForeignKey(cd => cd.CartId);
            });
        }
    }
}
