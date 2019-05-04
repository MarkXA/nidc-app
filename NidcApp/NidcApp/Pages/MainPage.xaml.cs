using NidcApp.MxaUi;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
			InitializeComponent();

		    AppState.DetailPage.SubscribeUi(p =>
		    {
		        Detail = new NavigationPage(p);
		        IsPresented = false;
		    });

		    AppState.NavigatePage.SubscribeUi(p =>
		    {
		        if (Detail is NavigationPage np)
		            np.Navigation.PushAsync(p);
		    });
		}
	}
}
