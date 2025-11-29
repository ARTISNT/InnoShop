using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Infrastructure.Db.Configurations;

public class ResetPasswordTokenConfiguration : IEntityTypeConfiguration<ResetPasswordToken>
{
    public void Configure(EntityTypeBuilder<ResetPasswordToken> builder)
    {
        builder.HasKey(rpt => rpt.Id); // 👈 Правильный PK

        builder.HasOne(rpt => rpt.User)
            .WithMany(u => u.ResetPasswordTokens)
            .HasForeignKey(rpt => rpt.UserId);

        builder.Property(rpt => rpt.Token)
            .IsRequired();
    }
}