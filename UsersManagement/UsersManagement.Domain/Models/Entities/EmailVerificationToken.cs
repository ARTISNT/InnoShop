namespace UsersManagement.Domain.Models.Entities;

public class EmailVerificationToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpiresOnUts { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserEntity User { get; set; }
}