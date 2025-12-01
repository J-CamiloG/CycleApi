using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Data;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        public ProductService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }


        public async Task<ProductReadDto?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);

            var spName = "Catalogo.sp_GetProductById";

            var product = await connection.QueryFirstOrDefaultAsync<Product>(
                spName,
                new { Id = id },
                commandType: System.Data.CommandType.StoredProcedure
            );

            if (product == null) return null;

            return new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                IsActive = product.IsActive,
                ImageBase64 = product.ImageBase64
            };
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var spName = "Catalogo.sp_GetProductos";

            var products = await connection.QueryAsync<Product>(
                spName,
                commandType: System.Data.CommandType.StoredProcedure 
            );

            return products.Select(p => new ProductReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category,
                IsActive = p.IsActive,
                ImageBase64 = p.ImageBase64
            });
        }


        public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageBase64 = dto.ImageBase64,

                Category = dto.Category,
                IsActive = dto.IsActive
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                IsActive = product.IsActive,
                ImageBase64 = product.ImageBase64
            };
        }

        public async Task<ProductReadDto?> UpdateAsync(int id, ProductUpdateDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.ImageBase64 = dto.ImageBase64;

            product.Category = dto.Category;
            product.IsActive = dto.IsActive;

            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                IsActive = product.IsActive,
                ImageBase64 = product.ImageBase64
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
