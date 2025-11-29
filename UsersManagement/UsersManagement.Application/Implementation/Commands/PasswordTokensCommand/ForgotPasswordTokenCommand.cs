using MediatR;

namespace UsersManagement.Application.Implementation.Commands.PasswordTokensCommand;

public record ForgotPasswordTokenCommand(string Email) : IRequest;