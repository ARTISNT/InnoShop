using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Domain.Models.Entities;
using UsersManagement.Infrastructure.Db.Context;

namespace UsersManagement.Infrastructure.Repositories;

public class UserRepository(UsersManagementDbContext usersManagementDbContext) : IUserRepository
{
    public async Task<IReadOnlyCollection<UserEntity>> GetAll()
    {
        return await usersManagementDbContext.Users.ToListAsync();
    }

    public async Task<UserEntity> GetById(Guid id)
    {
        return await usersManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Create(UserEntity user)
    {
        await usersManagementDbContext.Users.AddAsync(user);
        await usersManagementDbContext.SaveChangesAsync();
    }

    public async Task Update(UserEntity user)
    {
        var userEntity = await usersManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        
        userEntity.SetEmail(user.Email);
        userEntity.SetPassword(user.PasswordHash);
        userEntity.SetUsername(user.Username);
        
        await usersManagementDbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var  userEntity = await usersManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        usersManagementDbContext.Remove(userEntity);
        await usersManagementDbContext.SaveChangesAsync();
    }
}