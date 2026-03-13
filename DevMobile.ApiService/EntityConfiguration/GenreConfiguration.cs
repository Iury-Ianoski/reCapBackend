using DevMobile.ApiService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevMobile.ApiService.EntityConfiguration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
    }
}
