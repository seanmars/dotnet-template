using Dumpify;
using EfSqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(builder =>
{
    builder.AddConsole()
        .SetMinimumLevel(LogLevel.Information);
});

serviceCollection.AddDbContextPool<AppDbContext>(options =>
{
    options.UseSqlite("DataSource=file::memory:?cache=shared");

    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

var serviceProvider = serviceCollection.BuildServiceProvider();

await using var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.OpenConnectionAsync();
await dbContext.Database.EnsureCreatedAsync();

var data = new List<Item>()
{
    new Item { ItemId = 1, Amount = 10 },
    new Item { ItemId = 2, Amount = 20 },
    new Item { ItemId = 3, Amount = 30 },
    new Item { ItemId = 4, Amount = 40 },
    new Item { ItemId = 5, Amount = 50 },
};

await dbContext.Items.AddRangeAsync(data);
await dbContext.SaveChangesAsync();

int[] targets = [1, 3, 4];

var item = await dbContext.Items.Where(x => targets.Contains(x.ItemId)).ToListAsync();
item.DumpConsole();

foreach (var i in item)
{
    i.Amount += 1;
}

await dbContext.SaveChangesAsync();

item = await dbContext.Items.ToListAsync();
item.DumpConsole();

await dbContext.Database.EnsureDeletedAsync();