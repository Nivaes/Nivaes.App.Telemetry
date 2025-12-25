using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Nivaes.App.Telemetry;

public static class TelemetryBootstrap
{
    public static void Init(string serviceName, string otlpEndpoint)
    {
        Sdk.CreateTracerProviderBuilder()
            .AddSource(serviceName)
            .AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri(otlpEndpoint);
            })
            .Build();

        Sdk.CreateMeterProviderBuilder()
            .AddMeter(serviceName)
            .AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri(otlpEndpoint);
            })
            .Build();
    }
}

