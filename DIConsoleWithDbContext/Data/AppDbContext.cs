using Microsoft.EntityFrameworkCore;

namespace DIConsoleWithDbContext.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}