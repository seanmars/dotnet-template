using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace DIOpenTelemetry;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceName = Assembly.GetExecutingAssembly().GetName().Name ?? "DIOpenTelemetry";
        var serviceVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "1.0.0";

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddOpenTelemetry()
            .UseOtlpExporter()
            .WithTracing(tracing =>
            {
                tracing
                    .AddSource(serviceName)
                    .ConfigureResource(resource =>
                    {
                        resource.AddService(serviceName: serviceName, serviceVersion: serviceVersion);
                    })
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter();
            });
        
        builder.Services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));

        builder.Services.AddControllers();
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

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}