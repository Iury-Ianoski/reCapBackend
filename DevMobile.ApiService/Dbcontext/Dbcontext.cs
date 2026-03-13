using Microsoft.EntityFrameworkCore;
using DevMobile.ApiService.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=postgres");
}