using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB.Entities
{
    public partial class GCStoreContext : DbContext
    {
        public GCStoreContext()
        {
        }

        public GCStoreContext(DbContextOptions<GCStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<OrderOverView> OrderOverView { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Login> Login { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(13);

                entity.HasOne(d => d.FavoriteStoreNavigation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.FavoriteStore)
                    .HasConstraintName("FK__Customer__Favori__5535A963");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Produ__5165187F");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Store__5070F446");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.ProducutName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Order__60A75C0F");

                entity.HasOne(d => d.ProducutNameNavigation)
                    .WithMany(p => p.OrderItem)
                    .HasPrincipalKey(p => p.ProductName)
                    .HasForeignKey(d => d.ProducutName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__Produ__619B8048");
            });

            modelBuilder.Entity<OrderOverView>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__OrderOve__C3905BCFDF12EB54");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(7, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderOverView)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderOver__Custo__59063A47");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.OrderOverView)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderOver__Store__59FA5E80");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ProductName)
                    .HasName("UQ__Product__DD5A978A18ED8D03")
                    .IsUnique();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasIndex(e => e.StoreName)
                    .HasName("UQ__Store__520DB652F8238680")
                    .IsUnique();

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Postal)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.Name);
                entity.Property(e => e.Name)
                      .HasMaxLength(20);
                entity.Property(e => e.Password)
                      .IsRequired();
                      
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
