// See https://aka.ms/new-console-template for more information

async Task FooAsync(string info)
{
    Console.WriteLine($"{info}::Await Thread id: {Environment.CurrentManagedThreadId}");
    await Task.Run(() =>
    {
    });
}

Console.WriteLine($"Main Thread id: {Environment.CurrentManagedThreadId}");
await FooAsync("ConfigureAwait(true)").ConfigureAwait(true);
await FooAsync("ConfigureAwait(false)").ConfigureAwait(false);