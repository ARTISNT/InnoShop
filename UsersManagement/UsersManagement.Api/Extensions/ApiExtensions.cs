using System.Text.Json.Serialization;
using UsersManagement.Api.ExceptionHandling;

namespace UsersManagement.Api.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>())
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => c.UseInlineDefinitionsForEnums());
        services.AddHttpContextAccessor();
        services.AddHttpClient();
            
        return services;
    }
}