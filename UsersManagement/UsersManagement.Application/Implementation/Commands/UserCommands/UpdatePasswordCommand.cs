using MediatR;
using UsersManagement.Application.Dto.UserDto.PasswordDto;

namespace UsersManagement.Application.Implementation.Commands.UserCommands;

public record UpdatePasswordCommand(Guid Id, UpdateUserPasswordDto UpdateUserPasswordDto) :  IRequest;