using Microsoft.AspNetCore.Identity;
using UsersManagement.Api.Implementation;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Abstractions.UrlGenerator;
using UsersManagement.Application.Dto.UserDto.ProfileDto;
using UsersManagement.Application.Implementation.Queries.User;
using UsersManagement.Domain.Models.Entities;
using UsersManagement.Infrastructure.Repositories;

namespace UsersManagement.Api.Extensions;

public static class AddApplicationServices
{
    public static IServiceCollection AddCustomApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUrlGenerator, UrlGenerator>();
        services.AddScoped<IEmailVerificationTokenRepository, EmailVerificationTokenRepository>();
        services.AddScoped<IResetPasswordTokenRepository, ResetPasswordTokenRepository>();

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(GetAllUsersQuery).Assembly,
            typeof(Program).Assembly
        ));

        // AutoMapper
        services.AddAutoMapper(cfg => { }, typeof(UserResponseDto).Assembly);

        return services;
    }
}