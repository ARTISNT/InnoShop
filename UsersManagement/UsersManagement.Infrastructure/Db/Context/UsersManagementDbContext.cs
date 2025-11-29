using Microsoft.EntityFrameworkCore;
using UsersManagement.Domain.Models.Entities;
using UsersManagement.Infrastructure.Db.Configurations;

namespace UsersManagement.Infrastructure.Db.Context;

public class UsersManagementDbContext(DbContextOptions<UsersManagementDbContext> options) : DbContext(options)
{ 
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }
    public DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .Property(u => u.Role)
            .HasConversion(
                u => u.ToString(),
                u => Enum.Parse<UserRole>(u)
                );

        modelBuilder.ApplyConfiguration(new EmailVerificationTokenConfiguration());
        modelBuilder.ApplyConfiguration(new ResetPasswordTokenConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}