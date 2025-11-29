using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Dto.UserDto.ProfileDto;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; } 
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}