using DevMobile.ApiService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevMobile.ApiService.EntityConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(g => g.Name).IsRequired().HasMaxLength(100);

        builder.HasData(
            new Role{ Id = 1, Name = "Reader"},
            new Role{ Id = 2, Name = "Moderator"}
        );
    }
}
