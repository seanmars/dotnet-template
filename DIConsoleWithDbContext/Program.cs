// See https://aka.ms/new-console-template for more information

using DIConsoleWithDbContext;
using DIConsoleWithDbContext.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


const string EnvironmentVariableKey = "DOTNET_ENVIRONMENT";

static void ConfigureServices(IServiceCollection services, params string[] args)
{
    // Environment
    var environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableKey);

    // Configuration
    var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

    configurationBuilder.AddEnvironmentVariables();
    configurationBuilder.AddCommandLine(args);

    var configuration = configurationBuilder.Build();

    // Logging
    services.AddLogging(logging =>
    {
        logging.AddConsole()
            .AddConfiguration(configuration.GetSection("Logging"));
    });

    // Database
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<AppDbContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        if (environmentName == Environments.Development)
        {
            options.EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }
    });

    // Dependency
    services.AddSingleton<IRunner, Runner>();
}

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection, args);

var servicesProvider = serviceCollection.BuildServiceProvider();
var loggerFactory = servicesProvider.GetService<ILoggerFactory>();
var logger = loggerFactory?.CreateLogger("Main");
try
{
    var runner = servicesProvider.GetRequiredService<IRunner>();
    await runner.StartAsync();
}
catch (Exception ex)
{
    logger?.LogError("{ErrorMessage}", ex.ToString());
}