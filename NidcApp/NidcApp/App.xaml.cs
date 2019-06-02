using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using NidcApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace NidcApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start(
                "ios=979fd95e-807d-4d09-a499-1006fd2d2fee;android=d4106d76-b30d-4c45-9669-cf086c40c848",
                typeof(Analytics),
                typeof(Crashes));

            AppState.Init();
        }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}