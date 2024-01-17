// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using AutoMapper;
using UseAutoMapper;

var configuration = new MapperConfiguration(config =>
{
    config.AddProfile<UserProfile>();
});

var mapper = configuration.CreateMapper();

var src = new List<UserSource>();
for (var i = 0; i < 5; i++)
{
    src.Add(new()
    {
        Name = i.ToString(),
        CreatedTime = i,
        Value = i
    });
}

var result = mapper.Map<List<UserDest>>(src, options =>
{
    options.AfterMap((o, list) =>
    {
        // Generator random data id
        foreach (var data in list)
        {
            data.Id = new Random().Next(1, 100);
        }
    });
});

Console.WriteLine(JsonSerializer.Serialize(result));

namespace UseAutoMapper
{
    public class UserDest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public long CreatedTime { get; set; }
        public int Value { get; set; }
    }

    public class UserSource
    {
        public string Name { get; set; } = null!;
        public long CreatedTime { get; set; }
        public int Value { get; set; }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserSource, UserDest>();
        }
    }
}