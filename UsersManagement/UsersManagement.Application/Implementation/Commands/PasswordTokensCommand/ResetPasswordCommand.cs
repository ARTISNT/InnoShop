using MediatR;
using UsersManagement.Application.Dto.UserDto.PasswordDto;

namespace UsersManagement.Application.Implementation.Commands.PasswordTokensCommand;

public record ResetPasswordCommand(ResetUserPasswordDto ResetUserPasswordDto) :  IRequest<bool>;