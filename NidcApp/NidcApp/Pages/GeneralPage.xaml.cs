using NidcApp.Models;
using NidcApp.MxaUi;
using NidcApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ContentPage = NidcApp.Models.ContentPage;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralPage : GeneralPageBase
    {
        public GeneralPage(ContentPage h) : base(h)
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
                        }),
                    ViewModel.Markdown.SubscribeUi(
                        markdown =>
                        {
                            ScrollView.IsVisible = !string.IsNullOrWhiteSpace(markdown);
                            if (!string.IsNullOrWhiteSpace(markdown))
                                MarkdownView.Markdown = markdown;
                        })
                });
        }
    }

    public class GeneralPageBase : BaseContentPage<GeneralPageVm, ContentPage>
    {
        protected GeneralPageBase(ContentPage h) : base(h) { }
    }
}