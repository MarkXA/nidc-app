using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace NidcApp
{
	public partial class App : Application
	{
		public App ()
		{
#if DEBUG
		    //LiveReload.Init();
#endif
		    
		    InitializeComponent();

			MainPage = new Pages.MainPage();
		}

		protected override void OnStart ()
		{
		    AppState.Init();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
