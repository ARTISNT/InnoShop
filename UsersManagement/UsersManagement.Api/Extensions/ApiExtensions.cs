using System.Text.Json.Serialization;

namespace UsersManagement.Api.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => c.UseInlineDefinitionsForEnums());
        services.AddHttpContextAccessor();

        return services;
    }
}