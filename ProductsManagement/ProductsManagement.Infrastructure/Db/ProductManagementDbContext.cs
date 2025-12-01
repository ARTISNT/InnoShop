using Microsoft.EntityFrameworkCore;
using ProductsManagement.Domain.Models;

namespace ProductsManagement.Infrastructure.Db;

public class ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}