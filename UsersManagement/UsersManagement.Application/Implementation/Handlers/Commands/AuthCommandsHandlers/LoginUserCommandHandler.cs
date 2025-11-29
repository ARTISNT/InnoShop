using MediatR;
using Microsoft.AspNetCore.Identity;
using UsersManagement.Application.Abstractions.Jwt;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Implementation.Commands.AuthCommands;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Implementation.Handlers.Commands.AuthCommandsHandlers;

public class LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher<UserEntity> passwordHasher, IJwtProvider jwtProvider)
    : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.LoginUserDto;
        var user = await userRepository.GetByEmailAsync(dto.Email);
        if(user is null)
            throw new KeyNotFoundException($"User with email {dto.Email} not found");

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Success)
        {
            return await jwtProvider.GenerateToken(dto);
        }
        else
        {
            throw new UnauthorizedAccessException("Invalid password or email address.");
        }
    }
}