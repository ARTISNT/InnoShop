using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Abstractions.Repositories;

public interface IResetPasswordTokenRepository
{
    public Task<ResetPasswordToken?> GetByIdAsync(Guid id);
    public Task CreateAsync(ResetPasswordToken token);
    public Task Delete(ResetPasswordToken token);
}