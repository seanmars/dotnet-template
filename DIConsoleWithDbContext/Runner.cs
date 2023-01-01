using DIConsoleWithDbContext.Data;
using Microsoft.Extensions.Logging;

namespace DIConsoleWithDbContext;

public class Runner : IRunner
{
    private readonly ILogger<Runner> _logger;

    private readonly AppDbContext _dbContext;

    public Runner(ILogger<Runner> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task StartAsync(CancellationToken stoppingToken = default)
    {
        _logger.LogDebug("Runner Start");
        try
        {
            _logger.LogDebug("Database ProviderName::{Name}", _dbContext.Database.ProviderName);
            await Task.Delay(1000, stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("{ErrorMessage}", ex.ToString());
        }
        finally
        {
            _logger.LogDebug("Runner End");
        }
    }
}