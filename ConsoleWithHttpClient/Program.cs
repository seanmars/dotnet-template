using ConsoleWithHttpClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var currentEnvironment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT") ?? "Development";

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{currentEnvironment}.json", optional: true);

var configuration = configurationBuilder.Build();

var service = new ServiceCollection();

service.Configure<AppOptions>(configuration.GetSection("app"));

service.AddLogging(builder =>
{
    builder.AddSimpleConsole(config =>
        {
            config.SingleLine = true;
            config.TimestampFormat = "[HH:mm:ss] ";
        })
        .SetMinimumLevel(LogLevel.Debug);
});

service.AddHttpClient();
service.AddSingleton<Runner>();

await using var provider = service.BuildServiceProvider();
var runner = provider.GetRequiredService<Runner>();
await runner.Run();