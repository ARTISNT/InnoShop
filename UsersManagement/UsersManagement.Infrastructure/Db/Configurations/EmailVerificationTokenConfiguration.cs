using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Infrastructure.Db.Configurations;

public class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);
    }
}