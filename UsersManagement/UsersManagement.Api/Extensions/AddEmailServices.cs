namespace UsersManagement.Api.Extensions;

public static class AddEmailServices
{
    public static IServiceCollection AddCustomEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddFluentEmail(configuration["Email:SenderEmail"], configuration["Email:Sender"])
            .AddSmtpSender(
                configuration["Email:Host"],
                configuration.GetValue<int>("Email:Port"),
                configuration["Email:UserName"],
                configuration["Email:AppPassword"]
            );

        return services;
    }
}