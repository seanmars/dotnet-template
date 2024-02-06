namespace ImplementIDisposable;

public class ParentClass : IDisposable
{
    private bool _disposed = false;
    private readonly ManagedResource? _disposableResource = null!;

    public ParentClass()
    {
        _disposableResource = new ManagedResource();
        _disposableResource.ResourceName = "Parent Managed Resource";
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        Console.WriteLine($"Called {nameof(ParentClass)}.{nameof(Dispose)}");
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _disposableResource?.Dispose();
        }

        _disposed = true;
    }
}