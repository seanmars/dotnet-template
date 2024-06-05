using Spectre.Console;

await AnsiConsole.Progress()
    .Columns([
        new TaskDescriptionColumn(),
        new ProgressBarColumn(),
        new PercentageColumn(),
        new SpinnerColumn(),
    ])
    .StartAsync(async ctx =>
    {
        // Define tasks
        var task1 = ctx.AddTask("[green]Retriculating algorithms[/]");
        var task2 = ctx.AddTask("[green]Colliding splines[/]");

        while (!ctx.IsFinished)
        {
            await Task.Delay(50);
            task1.Increment(1.5);
            task2.Increment(2.8);
        }
    });