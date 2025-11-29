using MediatR;
using UsersManagement.Application.Dto.UserDto.ProfileDto;

namespace UsersManagement.Application.Implementation.Commands.UserCommands;

public record UpdateUserCommand(Guid Id, UpdateUserProfileDto UpdateUserProfileDto) : IRequest<Guid>;