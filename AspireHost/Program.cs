var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.OpenTelemetryNet>("OpenTelemetryNet");

builder.Build().Run();