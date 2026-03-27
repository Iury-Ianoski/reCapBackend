using Microsoft.EntityFrameworkCore;
using DevMobile.ApiService.Entities;

namespace DevMobile.ApiService.Dbcontext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}
