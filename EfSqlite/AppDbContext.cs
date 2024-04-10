using Microsoft.EntityFrameworkCore;

namespace EfSqlite;

public class AppDbContext : DbContext
{
    public DbSet<Item> Items => Set<Item>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}