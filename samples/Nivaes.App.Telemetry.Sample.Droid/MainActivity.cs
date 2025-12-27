using Android.Runtime;

namespace Nivaes.App.Telemetry.Sample.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //EditText nameEditText;
        //EditText emailEditText;
        //Button sendButton;
        //Button clearButton;
        Button? sendTrazeButton;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //// Referencias a los controles
            //nameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
            //emailEditText = FindViewById<EditText>(Resource.Id.emailEditText);
            //sendButton = FindViewById<Button>(Resource.Id.sendButton);
            //clearButton = FindViewById<Button>(Resource.Id.clearButton);
            sendTrazeButton = FindViewById<Button>(Resource.Id.sendTraze);

            //// Eventos
            //sendButton.Click += (s, e) =>
            //{
            //    Toast.MakeText(this, $"Nombre: {nameEditText.Text}\nEmail: {emailEditText.Text}", ToastLength.Long).Show();
            //};

            //clearButton.Click += (s, e) =>
            //{
            //    nameEditText.Text = "";
            //    emailEditText.Text = "";
            //};
            sendTrazeButton.Click += (s, e) =>
            {
                using var span = TelemetryActivity.Start("Feature.CreateInvoice");
            };

            AndroidEnvironment.UnhandledExceptionRaiser += static (o, e) =>
            {
                TelemetryExceptionHandler.RecordFatal(e.Exception, o?.GetType().FullName ?? "");
                e.Handled = true;
            };
        }
    }
}