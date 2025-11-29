namespace UsersManagement.Infrastructure.Settings;

public class AuthSettings
{
    public TimeSpan TokenLifetime { get; set; }
    public string SecretKey { get; set; }
}