using System.Diagnostics;

namespace Nivaes.App.Telemetry;

public static class TelemetryActivity
{
    public static Activity? Start(string name)
    {
        return TelemetryContext.Source.StartActivity(name);
    }
}
