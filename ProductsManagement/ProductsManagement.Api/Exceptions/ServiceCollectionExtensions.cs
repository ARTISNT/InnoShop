using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.Api.Extensions;
using ProductsManagement.Application.Abstractions;
using ProductsManagement.Application.Implementation.Cqrs.Handlers.Commands.Products;
using ProductsManagement.Application.Implementation.Cqrs.Queries.Products;
using ProductsManagement.Application.Implementation.Services;
using ProductsManagement.Infrastructure.Db;
using ProductsManagement.Infrastructure.Repositories;
using ProductsManagement.Infrastructure.Settings;

namespace ProductsManagement.Api.Exceptions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddControllersWithFiltersAndValidation(this IServiceCollection services)
        {
            services.AddControllers(cfg => cfg.Filters.Add<GlobalExceptionFilter>())
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<Program>();
                    });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = false;
            });

            return services;
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(CreateProductCommandHandler).Assembly,
                typeof(Program).Assembly));
            
            services.AddAutoMapper(cfg => { }, typeof(GetAllProductsQuery).Assembly);

            return services;
        }

        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
            services.AddAuth(configuration); 
            return services;
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static WebApplication UseSwaggerDocumentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }
    }
}
