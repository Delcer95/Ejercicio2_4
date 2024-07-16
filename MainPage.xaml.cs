using CommunityToolkit.Maui.Views;
using Ejercicio2_4.Models;
using Ejercicio2_4.Vistas;
using Xamarin.Essentials;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ejercicio2_4
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        string videoPath;

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            var videoOptions = new Xamarin.Essentials.MediaPickerOptions
            {
                Title = "Capturar video"
            };

            try
            {
                var video = await Xamarin.Essentials.MediaPicker.CaptureVideoAsync(videoOptions);

                if (video != null)
                {
                    using (var stream = await video.OpenReadAsync())
                    {
                        videoPath = await GetVideoPathAsync(stream);
                        img.Source = MediaSource.FromUri(new Uri(videoPath));
                    }
                }
            }
            catch (Xamarin.Essentials.FeatureNotSupportedException)
            {
                // La captura de video no es compatible en este dispositivo
                await DisplayAlert("Error", "La captura de video no es compatible en este dispositivo.", "Ok");
            }
            catch (Xamarin.Essentials.PermissionException)
            {
                // No se otorgó permiso para acceder a la cámara o al almacenamiento
                await DisplayAlert("Error", "No se otorgó permiso para acceder a la cámara o al almacenamiento.", "Ok");
            }
            catch (Exception ex)
            {
                // Otro error inesperado
                await DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "Ok");
            }
        }



        private async void btnSQlite_Clicked(object sender, EventArgs e)
        {
            // Verificar que se haya capturado un video
            if (string.IsNullOrEmpty(videoPath))
            {
                await DisplayAlert("Atencion", "Por favor, capture un video antes de guardar.", "Ok");
                return;
            }

            // Verificar que se hayan ingresado nombres y descripción
            if (string.IsNullOrWhiteSpace(txtnombre.Text) || string.IsNullOrWhiteSpace(txtdescripcion.Text))
            {
                await DisplayAlert("Atencion", "Por favor, ingrese nombres y descripción antes de guardar.", "Ok");
                return;
            }

            var empleado = new Empleados
            {
                nombres = txtnombre.Text,
                descripcion = txtdescripcion.Text,
                imagen = await File.ReadAllBytesAsync(videoPath)
            };

            var resultado = await App.BaseDatos.EmpleSave(empleado);

            if (resultado != 0)
            {
                await DisplayAlert("Atencion", "Tu Video fue ingresado correctamente!!!", "Ok");

                // Limpiar los campos y la imagen después de guardar
                txtnombre.Text = string.Empty;
                txtdescripcion.Text = string.Empty;
                videoPath = string.Empty;
                img.Source = null;
            }
            else
            {
                await DisplayAlert("Atencion", "Upps ha ocurrido un error inesperado", "Ok");
            }

            await Navigation.PopAsync();
        }

        private async Task<string> GetVideoPathAsync(Stream stream)
        {
            string videoFileName = "video.mp4";

            // Obtiene el directorio de almacenamiento específico de la aplicación
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string videoPath = Path.Combine(appDataDir, videoFileName);

            // Guarda el video en el directorio de almacenamiento específico de la aplicación
            using (FileStream fileStream = new FileStream(videoPath, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }

            return videoPath;
        }

        private async void btnLista_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaEmpleados());
        }


    }
}
