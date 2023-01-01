using System.Threading;
using System.Threading.Tasks;

namespace DependencyInjectionConsoleTemplate
{
    public interface IRunner
    {
        Task StartAsync(CancellationToken stoppingToken = default);
    }
}