using AutoMapper;
using MediatR;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Implementation.Commands.UserCommands;

namespace UsersManagement.Application.Implementation.Handlers.Commands.UserCommandsHandler;

public class DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper) 
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            throw new ArgumentException("Delete operation must have an Id.");
            
        await userRepository.Delete(request.Id);    
    }
}