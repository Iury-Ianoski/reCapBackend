using Microsoft.EntityFrameworkCore;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Dbcontext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();

    public DbSet<Genre> Genres => Set<Genre>();

    public DbSet<Review> Reviews => Set<Review>();
}
