// See https://aka.ms/new-console-template for more information
using Nivaes.App.Telemetry;

Console.WriteLine("Hello, telemetry!");

using var span = TelemetryActivity.Start("Feature.CreateInvoice");

Console.WriteLine("End telemetry");