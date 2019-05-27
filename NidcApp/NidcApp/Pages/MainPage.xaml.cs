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

                    AddPage("Agenda", () => new AgendaPage(), "agenda");
                    AddPage("On now", () => new TimeslotPage(GetNowId(conf)));
                    AddPage("On next", () => new TimeslotPage(GetNextId(conf)));
                    AddPage("Lightning talks", () => new LightningPage());
                    foreach (var page in conf.htmlpages)
                    {
                        AddPage(page.title, () => new WebViewPage(page));
                    }
                });
        }

        private void AddPage(string title, Func<object> content, string route = null)
        {
            var tab = new Tab();
            var shellContent = new ShellContent
            {
                ContentTemplate = new DataTemplate(content),
                Route = route ?? title.Replace(" ", "").ToLowerInvariant()
            };

            tab.Items.Add(shellContent);
            var flyoutItem = new FlyoutItem {Title = title};
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