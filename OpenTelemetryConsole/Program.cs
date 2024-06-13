using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("my-service"))
    .AddSource("my-service")
    .AddConsoleExporter()
    .Build();

using var activitySource = new ActivitySource("my-service");
using var span = activitySource.StartActivity("Main");
await Task.Delay(1000);
span?.AddEvent(new ActivityEvent("1"));

await Task.Delay(2000);
span?.AddEvent(new ActivityEvent("2"));