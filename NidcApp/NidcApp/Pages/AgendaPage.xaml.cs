using System;
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

            ListView.IsVisible = Device.RuntimePlatform == Device.iOS;
            CollectionView.IsVisible = Device.RuntimePlatform != Device.iOS;
            ListView.ItemTemplate = new DataTemplate(
                () => new ViewCell() {View = (View)CollectionView.ItemTemplate.CreateContent()});
        }

        private void ItemTapped(object sender, EventArgs e)
        {
            var timeSlot = (sender as BindableObject)?.BindingContext as TimeslotVm;
            if (!string.IsNullOrWhiteSpace(timeSlot?.TimeslotId))
                Shell.Current.GoToAsync($"//agenda/timeslot?slotId={timeSlot.TimeslotId}");
        }
    }

    public class AgendaPageBase : BaseContentPage<AgendaPageVm, Unit> { }
}