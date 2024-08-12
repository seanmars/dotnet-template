namespace AopSample;

public class ClassService
{
    private readonly ILogger<ClassService> _logger;

    public ClassService(ILogger<ClassService> logger)
    {
        _logger = logger;
    }

    public void MethodA()
    {
        _logger.LogInformation("MethodA called");
    }

    public void MethodB()
    {
        _logger.LogInformation("MethodB called");
    }
}