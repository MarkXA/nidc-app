using System.Reactive;
using NidcApp.MxaUi;
using NidcApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPage : FlyoutPageBase
    {
        public FlyoutPage()
        {
            InitializeComponent();
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is FlyoutViewModel.FlyoutMenuItem menuItem)
                AppState.DetailPage.OnNext(menuItem.CreatePage());

            ((ListView)sender).SelectedItem = null;
        }
    }

    public class FlyoutPageBase : BaseContentPage<FlyoutViewModel, Unit> { }
}