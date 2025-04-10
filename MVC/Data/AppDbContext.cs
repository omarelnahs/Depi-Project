using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.Price)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.Store)
                      .WithMany(s => s.Products)
                      .HasForeignKey(p => p.StoreId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasMaxLength(150)
                      .IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.TotalAmount)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(o => o.User)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(o => o.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(oi => oi.UnitPrice)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Product)
                      .WithMany()
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public DbSet<MVC.Models.CartItem> CartItem { get; set; } = default!;
    }
}
