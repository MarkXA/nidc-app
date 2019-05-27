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
        }

        public TimeslotPage(string timeSlotId) : base(timeSlotId)
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

    public class TimeslotPageBase : BaseContentPage<TimeslotPageVm, string>
    {
        protected TimeslotPageBase() { }
        protected TimeslotPageBase(string timeSlotId) : base(timeSlotId) { }
    }
}