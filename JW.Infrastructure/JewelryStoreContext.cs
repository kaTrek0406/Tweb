using System.Data.Entity;
using JW.Domain;

namespace JW.Infrastructure
{
    public class JewelryStoreContext : DbContext
    {
        public JewelryStoreContext() : base("name=JewelryStoreDbConnectionString")
        {
            // Конфигурация DbContext может быть здесь (например, логика миграции)
        }

        public DbSet<JewelryItem> JewelryItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReturnRequest> ReturnRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Настройте модель и отношения здесь с помощью Fluent API
            modelBuilder.Entity<JewelryItem>()
                .HasMany(j => j.Reviews)
                .WithRequired(r => r.JewelryItem)
                .HasForeignKey(r => r.JewelryItemId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithRequired(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            // Дополнительные настройки модели
        }
    }
}