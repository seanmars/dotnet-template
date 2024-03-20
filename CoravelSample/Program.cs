// https://docs.coravel.net/Scheduler/

using Coravel;
using Coravel.Invocable;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging(opt =>
        {
            opt.AddSimpleConsole(config =>
                {
                    config.SingleLine = true;
                    config.TimestampFormat = "[HH:mm:ss] ";
                })
                .SetMinimumLevel(LogLevel.Debug);
        });

        services.AddScheduler();
        services.AddTransient<SampleInvocable>();
    });

var host = hostBuilder.Build();

host.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<SampleInvocable>()
        .EveryFiveSeconds()
        .Once();

    scheduler
        .Schedule<SampleInvocable>()
        .DailyAt(0, 15)
        .Zoned(TimeZoneInfo.Local);
}).OnError(Console.WriteLine);

host.Run();

class SampleInvocable : IInvocable
{
    private readonly ILogger<SampleInvocable> _logger;

    public SampleInvocable(ILogger<SampleInvocable> logger)
    {
        _logger = logger;
    }

    public  Task Invoke()
    {
        _logger.LogDebug("Invocable executed!");
        // await Task.Delay(10000);
        return Task.CompletedTask;
    }
}