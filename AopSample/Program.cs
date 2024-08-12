/**
 * 如果需要 async 需要使用以下套件
 * https://github.com/JSkimming/Castle.Core.AsyncInterceptor
 */

using AopSample;
using Castle.DynamicProxy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IInterceptor, LoggingInterceptor>();
builder.Services.AddProxiedScoped<IDataService, DataService>();
// builder.Services.AddProxiedScoped<ClassService>();

builder.Services.AddControllers()
    .AddControllersAsServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();