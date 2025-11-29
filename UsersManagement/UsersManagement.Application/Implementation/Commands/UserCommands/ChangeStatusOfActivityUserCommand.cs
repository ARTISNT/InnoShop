using MediatR;

namespace UsersManagement.Application.Implementation.Commands.UserCommands;

public record ChangeStatusOfActivityUserCommand(Guid Id, bool IsActive) :  IRequest;