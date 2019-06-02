using System;
using NidcApp.MxaUi;
using NidcApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeslotPage : TimeslotPageBase
    {
        public TimeslotPage()
        {
            InitializeComponent();

            WhenActivated(
                () => new[]
                {
                    ViewModel.Timeslot.SubscribeUi(
                        vm =>
                        {
                            ListView.IsVisible = vm.HasSessions && Device.RuntimePlatform == Device.iOS;
                            CollectionView.IsVisible = vm.HasSessions && Device.RuntimePlatform != Device.iOS;
                        })
                });

            if (Device.RuntimePlatform == Device.iOS)
            {
                ListView.Margin = new Thickness(0, 70, 0, 0);
            }

            ListView.ItemTemplate = new DataTemplate(
                () => new ViewCell() {View = (View)CollectionView.ItemTemplate.CreateContent()});
        }

        public TimeslotPage(string timeSlotId) : base(timeSlotId)
        {
            InitializeComponent();
        }

        private void OnSpeakerTapped(object sender, EventArgs e)
        {
            if ((sender as BindableObject)?.BindingContext is SpeakerVm speaker)
                Shell.Current.GoToAsync($"speaker?speakerId={speaker.SpeakerId}");
        }
    }

    public class TimeslotPageBase : BaseContentPage<TimeslotPageVm, string>
    {
        protected TimeslotPageBase() { }
        protected TimeslotPageBase(string timeSlotId) : base(timeSlotId) { }
    }
}