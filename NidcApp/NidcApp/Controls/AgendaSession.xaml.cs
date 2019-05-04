using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendaSession : Grid
    {
        public static readonly BindableProperty RoomProperty = BindableProperty.Create(
            nameof(Room),
            typeof(string),
            typeof(AgendaSession));

        public string Room
        {
            get => (string)GetValue(RoomProperty);
            set => SetValue(RoomProperty, value);
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(AgendaSession));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty SessionIdProperty = BindableProperty.Create(
            nameof(SessionId),
            typeof(string),
            typeof(AgendaSession));

        public string SessionId
        {
            get => (string)GetValue(SessionIdProperty);
            set => SetValue(SessionIdProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == RoomProperty.PropertyName)
            {
                RoomLabel.Text = Room;
            }
            else if (propertyName == TitleProperty.PropertyName)
            {
                TitleLabel.Text = Title;
            }
        }

        public AgendaSession()
        {
            InitializeComponent();
        }
    }
}