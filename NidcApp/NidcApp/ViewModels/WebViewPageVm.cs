using NidcApp.Models;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class WebViewPageVm : BaseViewModel<HtmlPage>
    {
        public readonly MxaProperty<string> Title = "";
        public readonly MxaProperty<string> Html = "";

        public WebViewPageVm()
        {
            WhenActivated(
                disposables =>
                {
                    Title.Value = Parameter.title;
                    Html.Value = Parameter.html;
                });
        }
    }
}