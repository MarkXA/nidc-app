using NidcApp.Models;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class GeneralPageVm : BaseViewModel<InformationPage>
    {
        public readonly MxaProperty<string> Title = "";
        public readonly MxaProperty<string> Markdown = "";
        public readonly MxaProperty<string> Html = "";

        public GeneralPageVm()
        {
            WhenActivated(
                disposables =>
                {
                    Title.Value = Parameter.title;
                    Markdown.Value = Parameter.markdown;
                    Html.Value = Parameter.html;
                });
        }
    }
}