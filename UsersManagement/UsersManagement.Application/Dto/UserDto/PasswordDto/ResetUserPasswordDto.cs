namespace UsersManagement.Application.Dto.UserDto.PasswordDto;

public class ResetUserPasswordDto
{
    public Guid Token { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}