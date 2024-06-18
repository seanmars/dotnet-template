using System.Net.NetworkInformation;
using MediatR;

namespace WebAppMediaR;

public class WeatherRequestHandler : IRequestHandler<WeatherRequest, WeatherForecast>
{
    public Task<WeatherForecast> Handle(WeatherRequest request, CancellationToken cancellationToken)
    {
        var ping = new Ping();
        var reply = ping.Send("www.google.com");
        return Task.FromResult(new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = Convert.ToInt32(reply.RoundtripTime),
            Summary = reply.Status.ToString()
        });
    }
}