namespace ImplementIDisposable;

public class ManagedResource : IDisposable
{
    public string ResourceName { get; set; } = "Managed Resource";

    public void Dispose()
    {
        Console.WriteLine($"Disposed {ResourceName}");
    }
}