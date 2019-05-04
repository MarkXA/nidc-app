using System.Reactive;
using NidcApp.MxaUi;
using NidcApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaPage : AgendaPageBase
    {
        public AgendaPage()
        {
            InitializeComponent();
        }

        private void OnSessionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is TimeSlotVm timeSlot && !string.IsNullOrWhiteSpace(timeSlot.TimeSlotId))
                AppState.NavigatePage.OnNext(new TimeSlotPage(timeSlot.TimeSlotId));

            ((ListView)sender).SelectedItem = null;
        }
    }

    public class AgendaPageBase : BaseContentPage<AgendaViewModel, Unit> { }
}