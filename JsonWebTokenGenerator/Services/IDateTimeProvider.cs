namespace JsonWebTokenGenerator.Services;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}