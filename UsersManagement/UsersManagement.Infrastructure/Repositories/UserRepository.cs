using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Domain.Models.Entities;
using UsersManagement.Infrastructure.Db.Context;

namespace UsersManagement.Infrastructure.Repositories;

public class UserRepository(UsersManagementDbContext usersManagementDbContext) : IUserRepository
{
    public async Task<IReadOnlyCollection<UserEntity>> GetAllAsync()
    {
        return await usersManagementDbContext.Users.ToListAsync();
    }

    public async Task<UserEntity> GetByIdAsync(Guid id)
    {
        return await usersManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<UserEntity> GetByEmailAsync(string email)
    {
        return await usersManagementDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<bool> ExistByEmailAsync(string email)
    {
        return usersManagementDbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task CreateAsync(UserEntity user)
    {
        await usersManagementDbContext.Users.AddAsync(user);
        await usersManagementDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, UserEntity user)
    {
        var userEntity = await usersManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        userEntity?.SetEmail(user.Email);
        userEntity?.SetUsername(user.Username);
        
        await usersManagementDbContext.SaveChangesAsync();
    }

    public async Task ChangeStatusOfActivityAsync(Guid id, bool isActive)
    {
        var userEntity = await usersManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        if(isActive)
            userEntity?.Activate();
        else
            userEntity?.Deactivate();
        
        await usersManagementDbContext.SaveChangesAsync();
    }

    public async Task UpdatePasswordAsync(Guid id, string passwordHash)
    {
        var userEntity = await usersManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        userEntity?.SetPasswordHash(passwordHash);
        
        await usersManagementDbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var userEntity = await usersManagementDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        usersManagementDbContext.Remove(userEntity);
        await usersManagementDbContext.SaveChangesAsync();
    }
}