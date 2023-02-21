// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

IConfiguration BuildLoggerConfiguration()
{
    var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

    var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"serilog.{environment}.json", optional: true, reloadOnChange: true);

    return configurationBuilder.Build();
}

IHost CreateHost(params string[] arguments)
{
    var builder = Host.CreateDefaultBuilder(arguments)
        .ConfigureAppConfiguration((context, configuration) =>
        {
            var environment = context.HostingEnvironment;

            configuration.AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"serilog.{environment}.json", optional: true, reloadOnChange: true);
        })
        .ConfigureServices((context, services) =>
        {
        });

    builder.UseSerilog((context, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(context.Configuration);
    });

    return builder.Build();
}

async Task MainEntry(params string[] arguments)
{
    var topLogger = new LoggerConfiguration()
        .ReadFrom.Configuration(BuildLoggerConfiguration())
        .CreateLogger();

    try
    {
        topLogger.Information("Create {Name}", AppDomain.CurrentDomain.FriendlyName);

        var host = CreateHost(arguments);

        topLogger.Information("Start {Name}", AppDomain.CurrentDomain.FriendlyName);

        // await host.RunAsync();
    }
    catch (Exception e)
    {
        topLogger.Fatal("{ErrorMessage}", e.ToString());
    }
    finally
    {
        await Log.CloseAndFlushAsync();
    }
}

await MainEntry(args);