using NidcApp.MxaUi;
using NidcApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpeakerPage : SpeakerPageBase
    {
        public SpeakerPage(string SpeakerId) : base(SpeakerId)
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
        protected SpeakerPageBase(string SpeakerId) : base(SpeakerId) { }
    }
}