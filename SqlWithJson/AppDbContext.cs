using Microsoft.EntityFrameworkCore;

namespace SqlWithJson;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(64).IsRequired();
            entity.OwnsOne(e => e.Goods, b =>
            {
                b.OwnsMany(e => e.Items, build =>
                {
                    build.Property(x => x.ItemKey).HasJsonPropertyName("ItemId");
                });
                b.ToJson();
            });
        });
    }
}