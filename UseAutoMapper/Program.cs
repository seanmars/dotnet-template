// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using AutoMapper;

var configuration = new MapperConfiguration(config =>
{
    config.AddProfile<UserProfile>();
});

var mapper = configuration.CreateMapper();

var src = new UserSource()
{
    Name = "source name",
    CreatedTime = 1234567890,
    Value = int.MaxValue
};

var result = mapper.Map<UserDest>(src);

Console.WriteLine(JsonSerializer.Serialize(result));

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