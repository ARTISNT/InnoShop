using MediatR;
using Microsoft.AspNetCore.Identity;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Implementation.Commands.PasswordTokensCommand;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Implementation.Handlers.Commands.PasswordTokenCommandsHandler;

public class ResetPasswordCommandHandler(
    IResetPasswordTokenRepository resetPasswordTokenRepository,
    IUserRepository userRepository,
    IPasswordHasher<UserEntity> passwordHasher) 
    : IRequestHandler<ResetPasswordCommand, bool>
{
    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var dto = request.ResetUserPasswordDto;
        
        if (dto is null)
            throw new ArgumentNullException("Dto cannot be null", nameof(dto));

        var token = await resetPasswordTokenRepository.GetByIdAsync(dto.Token);
        
        if (token is null || token.ExpiresOnUts < DateTime.UtcNow)
            return false;

        if (dto.Password != dto.ConfirmPassword)
            throw new ArgumentException("Passwords do not match", nameof(dto.Password));
        
        await userRepository.UpdatePasswordAsync(token.UserId, passwordHasher.HashPassword(null!, dto.Password));
        token.Used = true;
        
        return true;
    }
}