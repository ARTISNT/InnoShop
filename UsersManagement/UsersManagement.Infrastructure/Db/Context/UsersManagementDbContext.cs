using Microsoft.EntityFrameworkCore;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Infrastructure.Db.Context;

public class UsersManagementDbContext(DbContextOptions<UsersManagementDbContext> options) : DbContext(options)
{ 
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .Property(u => u.Role)
            .HasConversion(
                u => u.ToString(),
                u => Enum.Parse<UserRole>(u)
                );
        
        base.OnModelCreating(modelBuilder);
    }
}