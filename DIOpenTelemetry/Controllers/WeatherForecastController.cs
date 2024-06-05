using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

namespace DIOpenTelemetry.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ActivitySource _activitySource;
    private readonly Tracer _tracer;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ActivitySource activitySource, Tracer tracer)
    {
        _logger = logger;
        _activitySource = activitySource;
        _tracer = tracer;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        using var activity = _tracer.StartSpan(nameof(Get));
        activity.SetAttribute("hello", "world");
        activity.SetAttribute("hello2", "world2");
        await Task.Delay(100);
        using var activity2 = _tracer.StartSpan("nested Get");
        activity2.SetAttribute("method", "GetWeatherForecast");
        await Task.Delay(300);
        activity2.End();
        activity.End();

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}