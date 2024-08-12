using Castle.DynamicProxy;

namespace AopSample;

[Serializable]
public class LoggingInterceptor : IInterceptor
{
    private readonly ILogger<LoggingInterceptor> _logger;

    public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
    {
        _logger = logger;
    }

    public void Intercept(IInvocation invocation)
    {
        var methodName = invocation.Method.Name;
        try
        {
            _logger.LogInformation("Entered Method:{MethodName}, Arguments: {Join}", methodName,
                string.Join(",", invocation.Arguments));
            invocation.Proceed();
            _logger.LogInformation("Successfully executed method:{MethodName}", methodName);
        }
        catch (Exception e)
        {
            _logger.LogInformation("Method:{MethodName}, Exception:{EMessage}", methodName, e.Message);
            throw;
        }
        finally
        {
            _logger.LogInformation("Exiting Method:{MethodName}", methodName);
        }
    }
}