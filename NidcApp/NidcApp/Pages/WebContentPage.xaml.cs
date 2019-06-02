using NidcApp.Models;
using NidcApp.MxaUi;
using NidcApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebContentPage : WebContentPageBase
    {
        public WebContentPage(InformationPage h) : base(h)
        {
            InitializeComponent();

            WhenActivated(
                () => new[]
                {
                    ViewModel.Title.SubscribeUi(title => Title = title),
                    ViewModel.Html.SubscribeUi(
                        html =>
                        {
                            HtmlView.IsVisible = !string.IsNullOrWhiteSpace(html);
                            if (!string.IsNullOrWhiteSpace(html))
                                HtmlView.Source = new HtmlWebViewSource {Html = html};
                        })
                });
        }
    }

    public class WebContentPageBase : BaseContentPage<GeneralPageVm, InformationPage>
    {
        protected WebContentPageBase(InformationPage h) : base(h) { }
    }
}