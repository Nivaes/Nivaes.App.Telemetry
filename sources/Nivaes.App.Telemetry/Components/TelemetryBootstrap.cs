using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Nivaes.App.Telemetry;

public static class TelemetryBootstrap
{
    public static void Init(string serviceName, string otlpEndpoint)
    {
        var tracer = Sdk.CreateTracerProviderBuilder()
            .AddSource(serviceName)
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(serviceName))
            .AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri(otlpEndpoint);
                //o.ExportProcessorType = ExportProcessorType.Batch;
                o.Protocol = OtlpExportProtocol.HttpProtobuf;
            })
            //.AddZipkinExporter(options =>
            //  {
            //      options.Endpoint = new Uri("your-zipkin-uri-here");
            //  })
            //.AddConsoleExporter()
            .Build();

        var trace = tracer.GetTracer("Traza inicio", "v1");
        using var span = trace.StartSpan("Spain inicio", SpanKind.Client);
        span.SetAttribute("clave", "valor");

        span.End();
        tracer.Dispose();

        var meter = Sdk.CreateMeterProviderBuilder()
            .AddMeter(serviceName)
            .AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri(otlpEndpoint);
                //o.ExportProcessorType = ExportProcessorType.Batch;
                o.Protocol = OtlpExportProtocol.HttpProtobuf;
            })
            .Build();

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddOpenTelemetry(logging =>
            {
                logging.AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(otlpEndpoint);
                    options.Protocol = OtlpExportProtocol.Grpc;
                });
            });
        });
    }
}

