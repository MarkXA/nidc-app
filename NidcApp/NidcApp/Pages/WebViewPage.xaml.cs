using NidcApp.Model;
using NidcApp.MxaUi;
using NidcApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewPage : WebViewPageBase
    {
        public WebViewPage(HtmlPage h) : base(h)
        {
            InitializeComponent();

            WhenActivated(
                () => new[]
                {
                    ViewModel.Title.SubscribeUi(title => Title = title),
                    ViewModel.Html.SubscribeUi(html => ZoomView.Source = new HtmlWebViewSource {Html = html})
                });
        }
    }

    public class WebViewPageBase : BaseContentPage<WebViewViewModel, HtmlPage>
    {
        protected WebViewPageBase(HtmlPage h) : base(h) { }
    }
}