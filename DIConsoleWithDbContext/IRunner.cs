namespace DIConsoleWithDbContext;

public interface IRunner
{
    Task StartAsync(CancellationToken stoppingToken = default);
}