using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UsersManagement.Application.Abstractions.Jwt;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Dto.UserDto;
using UsersManagement.Application.Dto.UserDto.AuthDto;
using UsersManagement.Infrastructure.Settings;

namespace UsersManagement.Infrastructure.Implementations.JwtProvider;

public class JwtProvider(IOptions<AuthSettings> options, IUserRepository userRepository) : IJwtProvider
{
    public async Task<string> GenerateToken(LoginUserDto loginUserDto)
    {
        var user = await userRepository.GetByEmailAsync(loginUserDto.Email);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, loginUserDto.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var jwtToken = new JwtSecurityToken(
            issuer: options.Value.Issuer,
            expires: DateTime.UtcNow.Add(options.Value.TokenLifetime),
            claims: claims,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)), 
                SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}