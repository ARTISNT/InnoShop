namespace UsersManagement.Domain.Models.Entities;

public class UserEntity
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    public UserRole Role { get; private set; } = UserRole.Seller;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public bool IsDeleted { get; private set; } =  false;

    private UserEntity() {} 

    public UserEntity(string username, string email, string passwordHash)
    {
        SetUsername(username);
        SetEmail(email);
        SetPassword(passwordHash);
    }

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

    public void SetPassword(string value)
    {
        if (value.Length < 20)
            throw new ArgumentException("Password hash too short.");
        PasswordHash = value;
    }
}

public enum UserRole
{
    Admin,
    Seller
}