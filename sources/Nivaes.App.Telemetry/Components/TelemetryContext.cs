using System.Diagnostics;

namespace Nivaes.App.Telemetry;

public static class TelemetryContext
{
    public static readonly ActivitySource Source =
        new("Nivaes.App");

    public static string SessionId { get; } =
        Guid.NewGuid().ToString();
}
