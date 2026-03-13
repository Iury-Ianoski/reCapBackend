using DevMobile.ApiService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevMobile.ApiService.EntityConfiguration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Author).IsRequired().HasMaxLength(100);
        builder.Property(b => b.PublicationYear).IsRequired();
        builder.Property(b => b.CoverImageUrl).HasMaxLength(500);
        builder.Property(b => b.Chapters).IsRequired();
        builder.Property(b => b.Summary).HasMaxLength(1000);

        builder.HasMany(b => b.Genres)
               .WithMany(g => g.Books)
               .UsingEntity(j => j.ToTable("BookGenres"));

        builder.HasMany(b => b.Reviews)
               .WithOne(r => r.Book)
               .HasForeignKey(r => r.BookId);

    }
}