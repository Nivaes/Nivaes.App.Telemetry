using System;
using System.Diagnostics;
using OpenTelemetry.Trace;

namespace Nivaes.App.Telemetry;

public static class TelemetryExceptionHandler
{
    public static void RegisterGlobalHandlers(string platform)
    {
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            if (e.ExceptionObject is Exception ex)
                RecordFatal(ex, platform + ".AppDomain");
        };

        TaskScheduler.UnobservedTaskException += (s, e) =>
        {
            RecordError(e.Exception, platform + ".TaskScheduler");
            e.SetObserved();
        };
    }

    public static void RecordFatal(Exception ex, string origin)
    {
        using var activity = TelemetryContext.Source
            .StartActivity("UnhandledException");

        activity?.AddException(ex);
        activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
        activity?.SetTag("exception.origin", origin);
        activity?.SetTag("exception.fatal", true);
        activity?.SetTag("session.id", TelemetryContext.SessionId);
        activity?.SetStatus(ActivityStatusCode.Error);

        //activity?.SetTag("device.os", DeviceInfo.Platform.ToString());
        //activity?.SetTag("app.version", AppInfo.VersionString);
    }

    public static void RecordError(Exception ex, string origin)
    {
        using var activity = TelemetryContext.Source
            .StartActivity("HandledException");

        activity?.AddException(ex);
        activity?.SetStatus(ActivityStatusCode.Error);
        activity?.SetTag("exception.origin", origin);
        activity?.SetTag("session.id", TelemetryContext.SessionId);
        activity?.SetStatus(ActivityStatusCode.Error);
    }
}
