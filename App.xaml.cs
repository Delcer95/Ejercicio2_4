using Ejercicio2_4.Controllers;

namespace Ejercicio2_4
{
    public partial class App : Application
    {
        static DatosDB basedatos;
        public static string VideoPath { get; set; }


        public static DatosDB BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new DatosDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EmpleadosDB.db3"));
                }
                return basedatos;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}