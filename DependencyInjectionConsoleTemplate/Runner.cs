using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionConsoleTemplate
{
    public class Runner : IRunner
    {
        private readonly ILogger<Runner> _logger;

        public Runner(ILogger<Runner> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken stoppingToken = default)
        {
            _logger.LogDebug("Runner Start");
            await Task.Delay(1000, stoppingToken);
            _logger.LogDebug("Runner End");
        }
    }
}