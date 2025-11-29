using MediatR;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Implementation.Commands.UserCommands;

namespace UsersManagement.Application.Implementation.Handlers.Commands.UserCommandsHandler;

public class ChangeStatusOfActivityUserCommandHandler(IUserRepository userRepository) : IRequestHandler<ChangeStatusOfActivityUserCommand>
{
    public async Task Handle(ChangeStatusOfActivityUserCommand request, CancellationToken cancellationToken)
    {
        if(request.Id == Guid.Empty)
            throw new ArgumentNullException("Request Id cannot be empty", nameof(request.Id));
        
        await userRepository.ChangeStatusOfActivityAsync(request.Id, request.IsActive);
    }
}