using BusinessObject.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace ServiceProduct
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductInfo> ProductInfo { get; set; } 
        public DbSet<Size> Sizes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
