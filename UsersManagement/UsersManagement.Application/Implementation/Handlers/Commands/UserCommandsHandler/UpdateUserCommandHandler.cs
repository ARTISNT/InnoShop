using AutoMapper;
using MediatR;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Implementation.Commands.UserCommands;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Implementation.Handlers.Commands.UserCommandsHandler;

public class UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper) 
    : IRequestHandler<UpdateUserCommand, Guid>
{
    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
       if(request.UpdateUserProfileDto is null) 
           throw new ArgumentNullException("Update user dto is null");
       
       var user = mapper.Map<UserEntity>(request.UpdateUserProfileDto);;
       await userRepository.UpdateAsync(request.Id, user);
       return user.Id;
    }
}