using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersProducts> OrdersProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().HasKey(p => p.product_id);
            modelBuilder.Entity<Cart>().HasKey(c => c.cart_id);
            modelBuilder.Entity<Customers>().HasKey(c => c.customer_id);
            modelBuilder.Entity<Login>().HasKey(l => l.login_id);
            modelBuilder.Entity<Orders>().HasKey(o => o.order_id);
            modelBuilder.Entity<OrdersProducts>().HasKey(op => op.order_product_id);

            // One-to-many relationship between Cart and Products
            modelBuilder.Entity<Cart>()
               .HasOne(c => c.Product)
               .WithMany(p => p.Cart)
               .HasForeignKey(c => c.product_id);

            // One-to-many relationship between Customer and Cart
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithMany(p => p.Cart)
                .HasForeignKey(c => c.customer_id);

            // One-to-One relationship between Customers and Login
            modelBuilder.Entity<Customers>()
                .HasOne(c => c.Login)
                .WithOne(l => l.Customers)
                .HasForeignKey<Login>(l => l.customer_id);

            // One-to-Many relationship between Customers and Orders
            modelBuilder.Entity<Orders>()
                .HasOne(c => c.Customers)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.customer_id);

            // One-to-Many relationship between OrdersProducts and Orders
            modelBuilder.Entity<OrdersProducts>()
                .HasOne(op => op.Orders)
                .WithMany(o => o.OrdersProducts)
                .HasForeignKey(op => op.order_id);

            // One-to-Many relationship between OrdersProducts and Products
            modelBuilder.Entity<OrdersProducts>()
                .HasOne(op => op.Products)
                .WithMany(p => p.OrdersProducts)
                .HasForeignKey(op => op.product_id);
        }

    }
}
