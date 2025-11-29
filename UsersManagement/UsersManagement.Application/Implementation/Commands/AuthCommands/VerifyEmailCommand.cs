using MediatR;

namespace UsersManagement.Application.Implementation.Commands.AuthCommands;

public record VerifyEmailCommand(Guid Token) : IRequest<bool>;