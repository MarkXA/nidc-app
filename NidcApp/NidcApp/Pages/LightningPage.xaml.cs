using System;
using System.Reactive;
using NidcApp.MxaUi;
using NidcApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LightningPage : LightningPageBase
    {
        public LightningPage()
        {
            InitializeComponent();
        }

        private void OnSessionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void OnSpeakerTapped(object sender, EventArgs e)
        {
            if ((sender as BindableObject)?.BindingContext is SpeakerVm speaker)
                AppState.NavigatePage.OnNext(new SpeakerPage(speaker.SpeakerId));
        }
    }

    public class LightningPageBase : BaseContentPage<LightningViewModel, Unit>{}
}