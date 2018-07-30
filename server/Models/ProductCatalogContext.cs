using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Utilities.Providers.DbConnectionProvider;

namespace WebApi.Models
{
    public partial class ProductCatalogContext : DbContext
    {
        public ProductCatalogContext()
        {
        }
        static string connectionString { get; set; }

        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                connectionString = DbConnectionFactory.CreateDefaultInstance().GetConnectionString();
                optionsBuilder.UseSqlServer(connectionString);
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //                optionsBuilder.UseSqlServer("Server=s0134dbtemp;Database=ProductCatalog;user=quantra; password=quantra2;pooling=true; Max Pool Size=100; Min Pool Size=8");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "Prd");

                entity.HasIndex(e => e.Code)
                    .IsUnique();

                entity.HasIndex(e => e.Name);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedUser)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(22, 2)");
            });
        }
    }
}
