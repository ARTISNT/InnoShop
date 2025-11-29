using MediatR;

namespace UsersManagement.Application.Implementation.Commands.UserCommands;

public record DeleteUserCommand(Guid Id) : IRequest;