using System.Diagnostics;
using Nivaes.App.Telemetry;

Console.WriteLine("Hello, telemetry!");

TelemetryBootstrap.Init("Nivaes.Console", "http://[::1]:4318");
//TelemetryBootstrap.Init("Nivaes.Console", "http://localhost:31674");

TelemetryExceptionHandler.RegisterGlobalHandlers("Console");

using var span = TelemetryActivity.Start("Console");
span?.SetTag("clave", "valor");
span?.AddEvent(new ActivityEvent("Evento"));


Task.Delay(1000).Wait();    

Console.WriteLine("End telemetry");