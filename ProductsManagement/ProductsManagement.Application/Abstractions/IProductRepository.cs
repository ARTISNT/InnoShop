using ProductsManagement.Application.Dto;
using ProductsManagement.Domain.Models;

namespace ProductsManagement.Application.Abstractions;

public interface IProductRepository
{
    public Task<ICollection<ProductEntity>> GetAllAsync(ProductFilteringDto productFilteringDto);
    public Task<ProductEntity> GetByIdAsync(Guid id);
    public Task SoftDeleteByUserIdAsync(Guid userId);
    public Task RestoreByUserIdAsync(Guid userId);
    public Task AddAsync(ProductEntity product);
    public Task UpdateAsync(Guid id, ProductEntity product);
    public Task UpdateStatusAsync(Guid id, bool status);
    public Task<bool> ExistsAsync(Guid id);
    public Task DeleteAsync(Guid id);
}