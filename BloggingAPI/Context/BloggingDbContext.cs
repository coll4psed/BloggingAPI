using BloggingAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloggingAPI.Context;

public class BloggingDbContext : DbContext
{
    public BloggingDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BloggingDatabase;Username=postgres;Password=12345678");
    }

    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
}
