using Microsoft.EntityFrameworkCore;
using ProductsManagement.Application.Abstractions;
using ProductsManagement.Application.Dto;
using ProductsManagement.Domain.Models;
using ProductsManagement.Infrastructure.Db;
using ProductsManagement.Infrastructure.Extensions;

namespace ProductsManagement.Infrastructure.Repositories;

public class ProductRepository(ProductManagementDbContext db) : IProductRepository
{
    public async Task<ICollection<ProductEntity>> GetAllAsync(ProductFilteringDto productFilteringDto)
    {
        var products = await db.Products.ApplyFiltering(productFilteringDto).ToListAsync();
        return products;
    }

    public async Task<ProductEntity> GetByIdAsync(Guid id)
    {
        var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product;
    }

    public async Task AddAsync(ProductEntity product)
    {
        await db.Products.AddAsync(product);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, ProductEntity product)
    {
        var updatedProduct = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
        
        updatedProduct.SetName(product.Name);
        updatedProduct.SetDescription(product.Description);
        updatedProduct.SetPrice(product.Price);
        
        await db.SaveChangesAsync();
    }
    
    public async Task SoftDeleteByUserIdAsync(Guid userId)
    {
        var products = await db.Products.Where(p => p.UserId == userId && !p.IsDeleted).ToListAsync();
        foreach (var product in products)
        {
            var entry = db.Products.Update(product);
            entry.Property(nameof(ProductEntity.IsDeleted)).CurrentValue = true;
        }

        await db.SaveChangesAsync();
    }

    public async Task RestoreByUserIdAsync(Guid userId)
    {
        var products = await db.Products.Where(p => p.UserId == userId && p.IsDeleted).ToListAsync();
        foreach (var product in products)
        {
            var entry = db.Products.Update(product);
            entry.Property(nameof(ProductEntity.IsDeleted)).CurrentValue = false;
        }

        await db.SaveChangesAsync();
    }
    
    public async Task UpdateStatusAsync(Guid id, bool status)
    {
        var product = db.Products.FirstOrDefault(p => p.Id == id);
        
        product.SetAvailability(status);
        await db.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
            return false;
        
        return true;
    }
    public async Task DeleteAsync(Guid id)
    {
        var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
        db.Products.Remove(product);
        await db.SaveChangesAsync();
    }
}