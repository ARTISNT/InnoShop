namespace ProductsManagement.Infrastructure.Settings;

public class AuthSettings
{
    public TimeSpan TokenLifetime { get; set; }
    public string SecretKey { get; set; }
    public string ValidIssuer { get; set; }
}