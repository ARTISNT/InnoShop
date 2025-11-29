using MediatR;
using UsersManagement.Application.Dto.UserDto.AuthDto;

namespace UsersManagement.Application.Implementation.Commands.AuthCommands;

public record RegisterUserCommand(RegisterUserDto RegisterUserDto) : IRequest;