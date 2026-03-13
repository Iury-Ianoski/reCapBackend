using DevMobile.ApiService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevMobile.ApiService.EntityConfiguration;

public class ReviewConfiguration: IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(r => r.Content).IsRequired();
        builder.Property(r => r.Chapter).IsRequired();
        builder.Property(r => r.Spoiler).IsRequired();
        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.BookId).IsRequired();
    }
}
