using DevMobile.ApiService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevMobile.ApiService.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Role).IsRequired().HasMaxLength(20);
        builder.Property(u => u.CreatedAt).IsRequired();

        builder.HasMany(u => u.Reviews)
               .WithOne(g => g.User)
               .HasForeignKey(r => r.UserId);
    }
}