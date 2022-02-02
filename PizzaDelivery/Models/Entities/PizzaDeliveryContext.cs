using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaDelivery.Models.Entities
{
    public partial class PizzaDeliveryContext : DbContext
    {
        public PizzaDeliveryContext()
        {
        }

        public PizzaDeliveryContext(DbContextOptions<PizzaDeliveryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=pizza-delivery-test.mysql.database.azure.com;user=raj_admin;password=Z6H4KkRkfM;database=pizza", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.32-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_general_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.StoreId, "store_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CompletedDate)
                    .HasColumnType("datetime(3)")
                    .HasColumnName("completed_date");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime(3)")
                    .HasColumnName("created_date");

                entity.Property(e => e.StoreId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("store_id");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("fk_order_store");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("order_products");

                entity.HasIndex(e => e.OrderId, "fk_orders_idx");

                entity.HasIndex(e => e.ProductId, "fk_products_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("order_id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("product_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.Desc)
                    .HasMaxLength(45)
                    .HasColumnName("desc");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Total).HasColumnName("total");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("store");

                entity.Property(e => e.Id)
                    .HasColumnType("int(10) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.StoreAddress)
                    .HasMaxLength(200)
                    .HasColumnName("store_address");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
