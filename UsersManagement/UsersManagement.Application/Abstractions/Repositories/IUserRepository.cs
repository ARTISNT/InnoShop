using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Abstractions.Repositories;

public interface IUserRepository
{
    public Task<IReadOnlyCollection<UserEntity>> GetAllAsync();
    public Task<UserEntity> GetByIdAsync(Guid id);
    public Task CreateAsync(UserEntity user);
    public Task UpdateAsync(Guid id, UserEntity user);
    public Task UpdatePasswordAsync(Guid id, string passwordHash);
    public Task ChangeStatusOfActivityAsync(Guid id, bool isActive);
    public Task Delete(Guid id);
    public Task<UserEntity> GetByEmailAsync(string email);
    public Task<bool> ExistByEmailAsync(string email);
}