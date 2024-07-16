using Android.App;
using Android.Runtime;
using Xamarin.Essentials;

namespace Ejercicio2_4
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Xamarin.Essentials.Platform.Init(this); // Agrega esta línea
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}