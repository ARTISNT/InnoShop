using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Domain.Models.Entities;
using UsersManagement.Infrastructure.Db.Context;

namespace UsersManagement.Infrastructure.Repositories;

public class EmailVerificationTokenRepository(UsersManagementDbContext usersManagementDbContext) : IEmailVerificationTokenRepository
{
    public async Task<EmailVerificationToken?> GetByIdAsync(Guid id)
    {
        var token = await usersManagementDbContext.EmailVerificationTokens
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        return token;
    }

    public async Task CreateAsync(EmailVerificationToken token)
    {
        await usersManagementDbContext.EmailVerificationTokens.AddAsync(token);
        await usersManagementDbContext.SaveChangesAsync();
    }

    public async Task Delete(EmailVerificationToken token)
    { 
        usersManagementDbContext.EmailVerificationTokens.Remove(token);
        await usersManagementDbContext.SaveChangesAsync();
    }
}