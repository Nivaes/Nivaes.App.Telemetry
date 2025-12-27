using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nivaes.App.Telemetry.Sample.WinUI3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            using var span = TelemetryActivity.Start("Guardar_Click");

            string nombre = txtNombre.Text;
            string apellidos = txtApellidos.Text;
            string edad = txtEdad.Text;
            string email = txtEmail.Text;

            // Aquí podrías guardar los datos en base de datos o mostrar un mensaje
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Datos guardados",
                Content = $"Nombre: {nombre}\nApellidos: {apellidos}\nEdad: {edad}\nEmail: {email}",
                CloseButtonText = "Ok"
            };

            _ = dialog.ShowAsync();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            using var span = TelemetryActivity.Start("Cancelar_Click");

            // Limpiar todos los campos
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtEdad.Text = "";
            txtEmail.Text = "";
        }

        private void Error_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
