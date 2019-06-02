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

            ListView.IsVisible = Device.RuntimePlatform == Device.iOS;
            CollectionView.IsVisible = Device.RuntimePlatform != Device.iOS;
            ListView.ItemTemplate = new DataTemplate(
                () => new ViewCell() {View = (View)CollectionView.ItemTemplate.CreateContent()});
        }

        private void OnSpeakerTapped(object sender, EventArgs e)
        {
            if ((sender as BindableObject)?.BindingContext is SpeakerVm speaker)
                Shell.Current.GoToAsync($"speaker?speakerId={speaker.SpeakerId}");
        }
    }

    public class LightningPageBase : BaseContentPage<LightningPageVm, Unit> { }
}