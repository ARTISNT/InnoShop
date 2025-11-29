using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Domain.Models.Entities;
using UsersManagement.Infrastructure.Db.Context;

namespace UsersManagement.Infrastructure.Repositories;

public class ResetPasswordTokenRepository(UsersManagementDbContext usersManagementDbContext) : IResetPasswordTokenRepository
{
    
    public async Task<ResetPasswordToken?> GetByIdAsync(Guid id)
    {
        var token = await usersManagementDbContext.ResetPasswordTokens
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        return token;
    }

    public async Task CreateAsync(ResetPasswordToken token)
    {
        await usersManagementDbContext.ResetPasswordTokens.AddAsync(token);
        await usersManagementDbContext.SaveChangesAsync();
    }

    public async Task Delete(ResetPasswordToken token)
    { 
        usersManagementDbContext.ResetPasswordTokens.Remove(token);
        await usersManagementDbContext.SaveChangesAsync();
    }
}