using NidcApp.Models;
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
                    ViewModel.Html.SubscribeUi(
                        html => ZoomView.Source = new HtmlWebViewSource
                        {
                            Html =
                                "<meta name='viewport' content='width=device-width, initial-scale=0.25, maximum-scale=3.0 user-scalable=1'>" +
                                html
                        })
                });
        }
    }

    public class WebViewPageBase : BaseContentPage<WebViewPageVm, HtmlPage>
    {
        protected WebViewPageBase(HtmlPage h) : base(h) { }
    }
}