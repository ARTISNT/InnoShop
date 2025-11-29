using FluentEmail.Core;
using MediatR;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Abstractions.UrlGenerator;
using UsersManagement.Application.Implementation.Commands.PasswordTokensCommand;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Implementation.Handlers.Commands.PasswordTokenCommandsHandler;

public class ForgotPasswordTokenCommandHandler(
    IFluentEmail fluentEmail,
    IUrlGenerator urlGenerator,
    IResetPasswordTokenRepository resetPasswordTokenRepository,
    IUserRepository userRepository
) : IRequestHandler<ForgotPasswordTokenCommand>
{
    public async Task Handle(ForgotPasswordTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user is null) return;

        var token = new ResetPasswordToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiresOnUts = DateTime.UtcNow.AddHours(1),
            Token = Guid.NewGuid().ToString("N"),
            Used = false,
        };

        await resetPasswordTokenRepository.CreateAsync(token);

        var url = urlGenerator.CreatePasswordResetToken(token);

        await fluentEmail
            .To(request.Email)
            .Subject("Reset password for inno shop")
            .Body($"To reset password go to this link <a href='{url}'>click here</a>", isHtml:  true)
            .SendAsync();
    }
}