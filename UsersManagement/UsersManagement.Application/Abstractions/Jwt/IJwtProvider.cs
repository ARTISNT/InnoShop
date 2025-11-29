using UsersManagement.Application.Dto.UserDto;
using UsersManagement.Application.Dto.UserDto.AuthDto;

namespace UsersManagement.Application.Abstractions.Jwt;

public interface IJwtProvider
{
    public Task<string> GenerateToken(LoginUserDto loginUserDto);
}