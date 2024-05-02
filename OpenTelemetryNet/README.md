# OpenTelemetry

使用 OpenTelemetry 搭配 Jaeger 為 .NET WebApi 應用程式增加 Trace 與 Metrics 功能。

## OpenTelemetry Packages

```shell
dotnet add package OpenTelemetry
dotnet add package OpenTelemetry.Exporter.Jaeger
dotnet add package OpenTelemetry.Exporter.Console
dotnet add package OpenTelemetry.Exporter.OpenTelemetryProtocol
dotnet add package OpenTelemetry.Instrumentation.AspNetCore
dotnet add package OpenTelemetry.Instrumentation.Http
dotnet add package OpenTelemetry.Instrumentation.EntityFrameworkCore
```

## Jaeger

- [Jaeger](https://www.jaegertracing.io/)

Run Jaeger in Docker With PowerShell:

```powershell
docker run -d --rm --name jaeger `
  -e COLLECTOR_ZIPKIN_HOST_PORT=:9411 `
  -p 6831:6831/udp `
  -p 6832:6832/udp `
  -p 5778:5778 `
  -p 16686:16686 `
  -p 4317:4317 `
  -p 4318:4318 `
  -p 14250:14250 `
  -p 14268:14268 `
  -p 14269:14269 `
  -p 9411:9411 `
  jaegertracing/all-in-one:latest
```

Or Bash:

```shell
docker run -d --rm --name jaeger \
  -e COLLECTOR_ZIPKIN_HOST_PORT=:9411 \
  -p 6831:6831/udp \
  -p 6832:6832/udp \
  -p 5778:5778 \
  -p 16686:16686 \
  -p 4317:4317 \
  -p 4318:4318 \
  -p 14250:14250 \
  -p 14268:14268 \
  -p 14269:14269 \
  -p 9411:9411 \
  jaegertracing/all-in-one:latest
```
