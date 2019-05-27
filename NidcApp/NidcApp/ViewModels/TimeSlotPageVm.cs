using System.Collections.Generic;
using System.Linq;
using NidcApp.MxaUi;
using Xamarin.Forms;

namespace NidcApp.ViewModels
{
    [QueryProperty("Parameter", "slotId")]
    public class TimeslotPageVm : BaseViewModel<string>
    {
        public MxaProperty<TimeslotVm> Timeslot { get; } = new MxaProperty<TimeslotVm>();

        public TimeslotPageVm()
        {
            WhenActivated(
                disposables =>
                {
                    AppState.Conference.SubscribeUi(
                        conf =>
                        {
                            Timeslot.Value =
                                conf.timeslots.Where(ts => ts.id == Parameter)
                                    .Select(ts => TimeslotVm.FromTimeslot(ts, conf))
                                    .FirstOrDefault() ?? new TimeslotVm
                                {
                                    TimeslotId = "",
                                    Description = "All quiet!",
                                    HasDescription = true,
                                    Title = "No conference",
                                    Sessions = new List<SessionVm>(),
                                    HasSessions = false
                                };
                        });
                });
        }
    }
}