using NidcApp.Model;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class WebViewViewModel : BaseViewModel<HtmlPage>
    {
        public MxaProperty<string> Title = "";
        public MxaProperty<string> Html = "";

        public WebViewViewModel()
        {
            WhenActivated(
                disposables =>
                {
                    Title.Value = Parameter.Title;
                    Html.Value = Parameter.Html;
                });
        }
    }
}