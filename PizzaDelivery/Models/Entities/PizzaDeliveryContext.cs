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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
