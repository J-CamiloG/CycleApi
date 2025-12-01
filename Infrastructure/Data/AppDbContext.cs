using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
             
                entity.ToTable("Productos", schema: "Catalogo");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name).HasColumnName("Nombre").HasMaxLength(100).IsRequired();

                entity.Property(x => x.Price).HasColumnName("Precio").HasPrecision(18, 2);

                entity.Property(x => x.Description).HasColumnName("Descripcion");

                entity.Property(x => x.Category).HasColumnName("Categoria");

                entity.Property(x => x.IsActive).HasColumnName("Estado");

                entity.Property(x => x.ImageBase64).HasColumnName("Imagen");

                entity.Ignore(x => x.CreatedAt);
                entity.Ignore(x => x.UpdatedAt);
            });
        }
    }
}