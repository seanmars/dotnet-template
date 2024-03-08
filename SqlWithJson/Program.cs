using Bogus;
using SqlWithJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

// Create user data by Bogus
var userFaker = new Faker<User>()
    .RuleFor(u => u.Name, f => f.Person.FullName)
    .RuleFor(u => u.Goods, f =>
    {
        var range = Enumerable.Range(1, 100);
        var randomized = f.Random.Shuffle(range).ToList();

        return new Goods()
        {
            Items = f.Make(f.Random.Int(1, 30), () => new Item
            {
                ItemId = f.PickRandom(randomized),
                Count = f.PickRandom(randomized)
            }).ToList(),
            Equipments = f.Make(f.Random.Int(1, 20), () => new Equipment
            {
                ItemId = f.PickRandom(randomized),
                Str = f.PickRandom(randomized),
                Dex = f.PickRandom(randomized),
                Wis = f.PickRandom(randomized),
            }).ToList()
        };
    });


var serviceProvider = serviceCollection.BuildServiceProvider();
await using var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.EnsureDeletedAsync();
await dbContext.Database.EnsureCreatedAsync();

await dbContext.Users.AddRangeAsync(userFaker.Generate(100));
await dbContext.SaveChangesAsync();