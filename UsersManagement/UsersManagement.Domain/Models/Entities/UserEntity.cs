namespace UsersManagement.Domain.Models.Entities;

public class UserEntity
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    public UserRole Role { get; private set; } = UserRole.Seller;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public bool IsActive { get; private set; } =  true;
    public bool EmailVerified { get; private set; } = false;
    
    public ICollection<ResetPasswordToken> ResetPasswordTokens { get; set; }
    public ICollection<EmailVerificationToken> EmailVerificationTokens { get; set; }

    private UserEntity() {} 

    public UserEntity(string username, string email, string passwordHash)
    {
        SetUsername(username);
        SetEmail(email);
        SetPasswordHash(passwordHash);
    }
    
    public void VerifyEmail() => EmailVerified = true;

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
    
    public void SetUsername(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Username cannot be null.");
        if (value.Length < 3)
            throw new ArgumentException("Username must be at least 3 characters.");
        Username = value;
    }

    public void SetEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be null.");
        Email = value;
    }

    public void SetPasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password hash cannot be null.");
        PasswordHash = value;
    }
}

public enum UserRole
{
    Admin,
    Seller
}