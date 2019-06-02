using NidcApp.Models;
using NidcApp.MxaUi;
using NidcApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkdownContentPage : MarkdownContentPageBase
    {
        public MarkdownContentPage(InformationPage h) : base(h)
        {
            InitializeComponent();

            WhenActivated(
                () => new[]
                {
                    ViewModel.Title.SubscribeUi(title => Title = title),
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

    public class MarkdownContentPageBase : BaseContentPage<GeneralPageVm, InformationPage>
    {
        protected MarkdownContentPageBase(InformationPage h) : base(h) { }
    }
}