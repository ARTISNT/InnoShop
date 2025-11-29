using MediatR;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Implementation.Commands.AuthCommands;

namespace UsersManagement.Application.Implementation.Handlers.Commands.AuthCommandsHandlers;

public class VerifyEmailCommandHandler(IEmailVerificationTokenRepository emailVerificationTokenRepository) 
    : IRequestHandler<VerifyEmailCommand, bool>
{
    public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        if (request.Token == Guid.Empty || request.Token == null)
            throw new ArgumentException("Not a valid token", nameof(request.Token));
           
        var emailVerificationToken = await emailVerificationTokenRepository.GetByIdAsync(request.Token);

        if (emailVerificationToken is null ||
            emailVerificationToken.ExpiresOnUts < DateTime.UtcNow ||
            emailVerificationToken.User.EmailVerified)
        {
            return false;
        }
        
        emailVerificationToken.User.VerifyEmail();

        await emailVerificationTokenRepository.Delete(emailVerificationToken);
        
        return true;
    }
}