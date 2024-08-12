namespace AopSample;

public interface IDataService
{
    Task<string> GetMessage();
}

public class DataService : IDataService
{
    protected ILogger<DataService> _logger;

    public DataService(ILogger<DataService> logger)
    {
        _logger = logger;
    }

    public async Task<string> GetMessage()
    {
        _logger.LogInformation("GetMessage called");
        await Task.Delay(1000);
        _logger.LogInformation("GetMessage called end");
        return "Hello World";
    }
}