using System.Runtime.InteropServices;
using MapKit;
using ObjCRuntime;
using static System.Net.Mime.MediaTypeNames;

namespace Nivaes.App.Telemetry.Sample.MacOS
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        public override void DidFinishLaunching(NSNotification notification)
        {
            NSSetUncaughtExceptionHandler(NativeUncaughtExceptionHandler);

            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                    TelemetryExceptionHandler.RecordFatal(ex, "iOS.UIThread");
            };

            TaskScheduler.UnobservedTaskException += (_, e) =>
            {
                TelemetryExceptionHandler.RecordFatal(e.Exception, "iOS.UIThread");
                e.SetObserved();
            };

            base.DidFinishLaunching(notification);
        }

        // P/Invoke for Objective-C NSSetUncaughtExceptionHandler
        private delegate void UncaughtExceptionHandlerDelegate(IntPtr exceptionPtr);

        [DllImport("/usr/lib/libobjc.dylib")]
        private static extern void NSSetUncaughtExceptionHandler(UncaughtExceptionHandlerDelegate handler);


        static void NativeUncaughtExceptionHandler(IntPtr exceptionPtr)
        {
            try
            {
                Exception ex;
                if (Runtime.GetNSObject(exceptionPtr) is NSException nsEx)
                {
                    ex = new Exception($"NSException: {nsEx?.Name} - {nsEx?.Reason}");
                }
                else
                {
                    ex = new Exception($"Uncaught NSException pointer: {exceptionPtr}");
                }
                TelemetryExceptionHandler.RecordFatal(ex, "iOS.UIThread");
            }
            catch (Exception ex)
            {
                TelemetryExceptionHandler.RecordFatal(ex, "iOS.NativeHandler");
            }
        }


        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
