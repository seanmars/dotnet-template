// https://docs.coravel.net/Scheduler/

using Coravel;
using Coravel.Invocable;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddScheduler();
        services.AddTransient<SampleInvocable>();
    });

var host = hostBuilder.Build();

host.Services.UseScheduler(scheduler =>
{
    scheduler
        .Schedule<SampleInvocable>()
        .EveryFiveSeconds();

    scheduler
        .Schedule<SampleInvocable>()
        .DailyAt(0, 15)
        .Zoned(TimeZoneInfo.Local);
});
host.Run();

class SampleInvocable : IInvocable
{
    public Task Invoke()
    {
        Console.WriteLine("Hello from Coravel!");
        return Task.CompletedTask;
    }
}