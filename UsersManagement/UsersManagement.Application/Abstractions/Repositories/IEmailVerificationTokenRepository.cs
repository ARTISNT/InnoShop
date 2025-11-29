using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Abstractions.Repositories;

public interface IEmailVerificationTokenRepository
{
    public Task<EmailVerificationToken?> GetByIdAsync(Guid id);
    public Task CreateAsync(EmailVerificationToken token);
    public Task Delete(EmailVerificationToken token);
}