using Microsoft.EntityFrameworkCore;
using UsersManagement.Infrastructure.Db.Context;

namespace UsersManagement.Api.Extensions;

public static class DataBaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UsersManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}