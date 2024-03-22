using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleWithHttpClient;

public class Runner
{
    private readonly ILogger _logger;
    private readonly AppOptions _options;

    private readonly HttpClient _httpClient;

    public Runner(ILogger<Runner> logger, IOptions<AppOptions> options, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _options = options.Value;

        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task Run()
    {
        _logger.LogInformation("Hello, {Name}", _options.ApiUrl);
        var content = await _httpClient.GetStringAsync(_options.ApiUrl);
        _logger.LogInformation("Response: {Content}", content);
    }
}