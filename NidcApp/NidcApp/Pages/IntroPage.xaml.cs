using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroPage : ContentPage
    {
        public static ICommand SponsorTapped { get; set; } = new Command(
            async url => await Browser.OpenAsync(url.ToString(), BrowserLaunchMode.SystemPreferred));

        public IntroPage()
        {
            InitializeComponent();
        }
    }
}