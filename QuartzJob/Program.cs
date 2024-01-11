using Quartz;
using QuartzJob.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz(q =>
{
    q.UseSimpleTypeLoader();
    q.UseInMemoryStore();
    
    // convert time zones using converter that can handle Windows/Linux differences
    // integration https://github.com/mattjohnsonpint/TimeZoneConverter
    // q.UseTimeZoneConverter();
    
    // auto-interrupt long-running job
    // q.UseJobAutoInterrupt(options =>
    // {
    //     // this is the default
    //     options.DefaultMaxRunTime = TimeSpan.FromMinutes(5);
    // });
    
    q.AddJob<HelloWorldJob>(opts =>
    {
        opts.StoreDurably();
    });
});

// Add the Quartz.NET hosted service
builder.Services.AddQuartzHostedService(
    q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();