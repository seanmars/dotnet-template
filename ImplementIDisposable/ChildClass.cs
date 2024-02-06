namespace ImplementIDisposable;

public class ChildClass : ParentClass
{
    private bool _disposed = false;
    private readonly ManagedResource? _childManagedResource = null!;

    public ChildClass()
    {
        _childManagedResource = new ManagedResource();
        _childManagedResource.ResourceName = "Child Managed Resource";
    }

    public void DoSomething()
    {
        Console.WriteLine("Do something...");
    }

    protected override void Dispose(bool disposing)
    {
        Console.WriteLine($"Called {nameof(ChildClass)}.{nameof(Dispose)}");
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _childManagedResource?.Dispose();
        }

        _disposed = true;
        base.Dispose(disposing);
    }
}