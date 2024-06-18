using MediatR;

namespace WebAppMediaR;

public class WeatherRequest : IRequest<WeatherForecast>
{
}