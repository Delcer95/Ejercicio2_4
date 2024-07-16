using Ejercicio2_4.Models;

namespace Ejercicio2_4.Vistas;

 public partial class ListaEmpleados : ContentPage
    {
        public ListaEmpleados()
        {
            InitializeComponent();
        }

        private async void toolmenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listapersonas.ItemsSource = await App.BaseDatos.obtenerListaEmple();
        }

        private async void liestapersonas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var persona = (Empleados)e.Item;

                var result = await DisplayActionSheet("Selecciona una opción", "Cancelar", null, "Ver Video", "Borrar");

                if (result == "Ver Video")
                {
                    Vista_Video page = new Vista_Video();
                    page.BindingContext = persona;
                    await Navigation.PushAsync(page);
                }
                else if (result == "Borrar")
                {
                    await App.BaseDatos.EmpleDelete(persona);
                    listapersonas.ItemsSource = await App.BaseDatos.obtenerListaEmple();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar ItemTapped: {ex.Message}");
            }
        }
    }
    
