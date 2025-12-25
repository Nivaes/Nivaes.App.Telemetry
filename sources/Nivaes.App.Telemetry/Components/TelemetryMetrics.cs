using System.Diagnostics.Metrics;

namespace Nivaes.App.Telemetry;

public static class TelemetryMetrics
{
    private static readonly Meter Meter =
        new("Nivaes.Metrics");

    private static readonly Counter<long> FeatureUsage =
        Meter.CreateCounter<long>("feature_usage");

    public static void FeatureUsed(string feature, string result)
    {
        FeatureUsage.Add(1,
            new("feature", feature),
            new("result", result));
    }
}
