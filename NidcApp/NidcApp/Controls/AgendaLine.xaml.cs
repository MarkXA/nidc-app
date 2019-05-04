using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaLine : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(AgendaLine));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                LineLabel.Text = Text;
            }
        }

        public AgendaLine()
        {
            InitializeComponent();
        }
    }
}