namespace UsersManagement.Domain.Models.Entities;

public class ResetPasswordToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Token { get; set; }
    public DateTime ExpiresOnUts { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Used { get; set; }
    public UserEntity User { get; set; }
}