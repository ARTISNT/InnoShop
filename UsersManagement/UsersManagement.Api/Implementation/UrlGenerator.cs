using UsersManagement.Application.Abstractions.UrlGenerator;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Api.Implementation;

public class UrlGenerator(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator) : IUrlGenerator
{
    public string CreateEmailVerificationToken(EmailVerificationToken emailVerificationToken)
    {
        string? verificationToken = linkGenerator.GetUriByName(
            httpContextAccessor.HttpContext!,
            "VerifyEmail",
            new { token = emailVerificationToken.Id });
        
        return verificationToken ?? throw new Exception("Could not generate email verification token");
    }

    public string CreatePasswordResetToken(ResetPasswordToken resetPasswordToken)
    {
        string? url = linkGenerator.GetUriByName(
            httpContextAccessor.HttpContext!,
            "ForgotPassword",
            new { token = resetPasswordToken.Id });
        
        return url ?? throw new Exception("Could not generate email verification token");
    }
}