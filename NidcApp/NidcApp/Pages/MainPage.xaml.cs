using System;
using System.Linq;
using NidcApp.Models;
using NidcApp.MxaUi;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NidcApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();

            Routing.RegisterRoute("timeslot", typeof(TimeslotPage));

            AppState.Conference.SubscribeUi(
                conf =>
                {
                    foreach (var item in TheShell.Items.Where(si => si.Title != "NIDC 2019").ToList())
                        TheShell.Items.Remove(item);

                    AddPage("Agenda", () => new AgendaPage(), "icon_agenda.png", "agenda");
                    AddPage("On now", () => new TimeslotPage(GetNowId(conf)), "icon_now.png");
                    AddPage("On next", () => new TimeslotPage(GetNextId(conf)), "icon_next.png");
                    AddPage("Lightning talks", () => new LightningPage(), "icon_lightning.png");
                    foreach (var page in conf.contentpages)
                    {
                        AddPage(
                            page.title,
                            () => string.IsNullOrWhiteSpace(page.markdown)
                                ? (ContentPage)new WebContentPage(page)
                                : new MarkdownContentPage(page),
                            "icon_info.png");
                    }

                    Routing.RegisterRoute("timeslot", typeof(TimeslotPage));
                    Routing.RegisterRoute("speaker", typeof(SpeakerPage));

                    //TheShell.Navigated += (sender, args) => TheShell.FlyoutIsPresented = false;
                });
        }

        private void AddPage(string title, Func<object> content, string iconPath = null, string route = null)
        {
            var tab = new Tab();
            var shellContent = new ShellContent
            {
                //Content = content(),
                ContentTemplate = new DataTemplate(content),
                Route = route ?? title.Replace(" ", "").ToLowerInvariant()
            };

            tab.Items.Add(shellContent);
            var flyoutItem = new FlyoutItem {Title = title, Icon = ImageSource.FromFile(iconPath)};
            flyoutItem.Items.Add(tab);
            TheShell.Items.Insert(TheShell.Items.Count - 1, flyoutItem);
        }

        private string GetNowId(Conference conf)
        {
            var now = DateTime.UtcNow;
            var endTime = ToDateTime(conf.date, conf.timeslots.Last().endTime);
            if (now > endTime)
                return null;

            var timeSlot = conf.timeslots.LastOrDefault(t => ToDateTime(conf.date, t.startTime) < now);
            return timeSlot?.id;
        }

        private string GetNextId(Conference conf)
        {
            var now = DateTime.UtcNow;
            var timeSlot = conf.timeslots.FirstOrDefault(t => ToDateTime(conf.date, t.startTime) > now);
            return timeSlot?.id;
        }

        private DateTime ToDateTime(string date, string time)
        {
            return TimeZoneInfo.ConvertTimeToUtc(
                DateTime.Parse(date + " " + time),
                TimeZoneInfo.FindSystemTimeZoneById("Europe/London"));
        }
    }
}