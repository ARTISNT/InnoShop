using FluentEmail.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Abstractions.UrlGenerator;
using UsersManagement.Application.Implementation.Commands.AuthCommands;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Implementation.Handlers.Commands.AuthCommandsHandlers;

public class RegisterUserCommandHandler(
    IUserRepository userRepository, 
    IPasswordHasher<UserEntity> passwordHasher, 
    IFluentEmail fluentEmail,
    IUrlGenerator urlGenerator,
    IEmailVerificationTokenRepository emailVerificationTokenRepository
    ) 
    :  IRequestHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.RegisterUserDto;

        if (await userRepository.ExistByEmailAsync(dto.Email))
            throw new ArgumentException("Email already exists");

        string passHash = passwordHasher.HashPassword(null!, dto.Password);

        var user = new UserEntity(dto.Username, dto.Email, passHash);
        await userRepository.CreateAsync(user);

        var emailVerificationToken = new EmailVerificationToken
        {
            CreatedAt = DateTime.UtcNow,
            ExpiresOnUts = DateTime.UtcNow.AddHours(1),
            Id = Guid.NewGuid(),
            UserId = user.Id,
        };
        await emailVerificationTokenRepository.CreateAsync(emailVerificationToken);
        
        var verificationLink = urlGenerator.CreateEmailVerificationToken(emailVerificationToken); 
        
        await fluentEmail
            .To(request.RegisterUserDto.Email)
            .Subject("Email verification for inno shop")
            .Body($"To verify your email go to this link <a href='{verificationLink}'>click here</a>", isHtml:  true)
            .SendAsync();

    }
}