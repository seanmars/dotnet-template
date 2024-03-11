using Bogus;
using Dumpify;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlWithJson;

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(builder =>
{
    builder.AddConsole()
        .SetMinimumLevel(LogLevel.Debug);
});

serviceCollection.AddDbContextPool<AppDbContext>(options =>
{
    options.UseSqlite("DataSource=./storage/sqlite-json.db;Cache=Shared");

    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});

var serviceProvider = serviceCollection.BuildServiceProvider();

// await CreateSomeData(serviceProvider);
await FindSomeData(serviceProvider);

async Task FindSomeData(IServiceProvider sp)
{
    await using var dbContext = sp.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();

    var users = await dbContext.Users
        .AsNoTracking()
        .Where(u => u.Goods.Items != null && u.Goods.Items.Any(eq => eq.Count > 50))
        .ToListAsync();

    users.Count.DumpConsole();
}

async Task CreateSomeData(IServiceProvider sp)
{
    // Create user data by Bogus
    var faker = new Faker<User>()
        .RuleFor(u => u.Name, f => f.Person.FullName)
        .RuleFor(u => u.Goods, f =>
        {
            var range = Enumerable.Range(1, 100);
            var randomized = f.Random.Shuffle(range).ToList();

            // return new Goods()
            // {
            //     Items = f.Make(f.Random.Int(1, 30), () => new Item
            //     {
            //         ItemKey = f.PickRandom(randomized),
            //         Count = f.PickRandom(randomized)
            //     }).ToList(),
            // };

            return new Goods()
            {
                Items = f.Make(f.Random.Int(1, 30),
                    () => new Item
                    {
                        ItemKey = f.PickRandom(randomized),
                        Count = f.PickRandom(randomized)
                    }).ToList(),
            };
        });

    await using var dbContext = sp.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.EnsureCreatedAsync();

    await dbContext.Users.AddRangeAsync(faker.Generate(1000));
    await dbContext.SaveChangesAsync();
}