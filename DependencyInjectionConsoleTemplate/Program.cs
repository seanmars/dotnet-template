// See https://aka.ms/new-console-template for more information

using DependencyInjectionConsoleTemplate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

const string EnvironmentVariableKey = "DOTNET_ENVIRONMENT";

static void ConfigureServices(IServiceCollection services, string[] args)
{
    // Environment
    var environmentName = Environment.GetEnvironmentVariable(EnvironmentVariableKey);

    // Configuration
    var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

    configurationBuilder.AddEnvironmentVariables();

    if (args != null)
    {
        configurationBuilder.AddCommandLine(args);
    }

    var configuration = configurationBuilder.Build();

    // Logging
    services.AddLogging(logging =>
    {
        logging.AddConsole()
            .AddConfiguration(configuration.GetSection("Logging"));
    });

    // Dependency
    services.AddSingleton<IRunner, Runner>();
}

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection, args);

var servicesProvider = serviceCollection.BuildServiceProvider();

var runner = servicesProvider.GetRequiredService<IRunner>();
await runner.StartAsync();