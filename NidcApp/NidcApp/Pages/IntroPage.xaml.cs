using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        private void BazaarvoiceTapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.bazaarvoice.com/"));
        }

        private void AllstateTapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.allstate.com/northern-ireland.aspx"));
        }

        private void LibertyTapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.liberty-it.co.uk/"));
        }

        private void FlexeraTapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.flexera.com/"));
        }

        private void PwcTapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.pwc.co.uk/who-we-are/regional-sites/northern-ireland.html"));
        }

        private void SpotxTapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.spotx.tv/"));
        }

        private void SmashflyTapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.smashfly.com/"));
        }

        private void PinchGestureRecognizer_OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            Trace.Write(e.Status);
        }
    }
}