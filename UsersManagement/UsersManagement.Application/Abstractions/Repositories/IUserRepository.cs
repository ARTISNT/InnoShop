using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Abstractions.Repositories;

public interface IUserRepository
{
    public Task<IReadOnlyCollection<UserEntity>> GetAll();
    public Task<UserEntity> GetById(Guid id);
    public Task Create(UserEntity user);
    public Task Update(UserEntity user);
    public Task Delete(Guid id);
}