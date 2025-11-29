using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Abstractions.UrlGenerator;

public interface IUrlGenerator
{
    public string CreateEmailVerificationToken(EmailVerificationToken emailVerificationToken);
    public string CreatePasswordResetToken(ResetPasswordToken resetPasswordToken);
}