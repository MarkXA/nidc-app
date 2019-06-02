using NidcApp.MxaUi;
using NidcApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpeakerPage : SpeakerPageBase
    {
        public SpeakerPage()
        {
            InitializeComponent();
        }

        public SpeakerPage(string speakerId) : base(speakerId)
        {
            InitializeComponent();
        }

        private void OnSessionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }

    public class SpeakerPageBase : BaseContentPage<SpeakerPageVm, string>
    {
        protected SpeakerPageBase() { }
        protected SpeakerPageBase(string speakerId) : base(speakerId) { }
    }
}