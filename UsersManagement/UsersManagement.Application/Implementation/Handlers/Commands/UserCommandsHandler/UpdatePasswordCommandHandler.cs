using MediatR;
using Microsoft.AspNetCore.Identity;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Implementation.Commands.UserCommands;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Implementation.Handlers.Commands.UserCommandsHandler;

public class UpdatePasswordCommandHandler(IUserRepository userRepository, IPasswordHasher<UserEntity> passwordHasher) : IRequestHandler<UpdatePasswordCommand>
{
    public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var dto = request.UpdateUserPasswordDto;
        
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));
        
        if(request.Id == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(request.Id));
            
        if (dto.Password != dto.ConfirmPassword)
            throw new ArgumentException("Passwords do not match", nameof(dto.Password));
        
        await userRepository.UpdatePasswordAsync(request.Id, passwordHasher.HashPassword(null!, dto.Password));
    }
}