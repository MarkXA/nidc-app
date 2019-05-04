using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using NidcApp.Model;
using NidcApp.MxaUi;
using NidcApp.Pages;
using Xamarin.Forms;

namespace NidcApp.ViewModels
{
    public class FlyoutViewModel : BaseViewModel<Unit>
    {
        public class FlyoutMenuItem
        {
            public string Title { get; set; }
            public Func<Page> CreatePage { get; set; }
        }

        public MxaProperty<List<FlyoutMenuItem>> MenuItems { get; } = new List<FlyoutMenuItem>();

        public FlyoutViewModel()
        {
            WhenActivated(
                () => new[]
                {
                    AppState.Conference.SubscribeUi(
                        conf =>
                        {
                            MenuItems.Value =
                                new[]
                                    {
                                        new FlyoutMenuItem {Title = "Agenda", CreatePage = () => new AgendaPage()},
                                        new FlyoutMenuItem
                                        {
                                            Title = "On now",
                                            CreatePage = () => new TimeSlotPage(GetNowId(conf))
                                        },
                                        new FlyoutMenuItem
                                        {
                                            Title = "On next",
                                            CreatePage = () => new TimeSlotPage(GetNextId(conf))
                                        },
                                        new FlyoutMenuItem
                                        {
                                            Title = "Lightning talks",
                                            CreatePage = () => new LightningPage()
                                        }
                                    }.Concat(
                                        conf.HtmlPages.Select(
                                            h => new FlyoutMenuItem
                                            {
                                                Title = h.Title,
                                                CreatePage = () => new WebViewPage(h)
                                            }))
                                    .Concat(
                                        new[]
                                        {
                                            new FlyoutMenuItem {Title = "NIDC 2018", CreatePage = () => new IntroPage()}
                                        })
                                    .ToList();
                        })
                });
        }

        private string GetNowId(Conference conf)
        {
            var now = DateTime.UtcNow;
            var endTime = ToDateTime(conf.Date, conf.TimeSlots.Last().EndTime);
            if (now > endTime)
                return null;

            var timeSlot = conf.TimeSlots.LastOrDefault(t => ToDateTime(conf.Date, t.StartTime) < now);
            return timeSlot?.TimeSlotId;
        }

        private string GetNextId(Conference conf)
        {
            var now = DateTime.UtcNow;
            var timeSlot = conf.TimeSlots.FirstOrDefault(t => ToDateTime(conf.Date, t.StartTime) > now);
            return timeSlot?.TimeSlotId;
        }

        private DateTime ToDateTime(string date, string time)
        {
            return TimeZoneInfo.ConvertTimeToUtc(
                DateTime.Parse(date + " " + time),
                TimeZoneInfo.FindSystemTimeZoneById("Europe/London"));
        }
    }
}