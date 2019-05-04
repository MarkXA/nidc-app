using System.Collections.Generic;
using System.Linq;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class TimeSlotViewModel : BaseViewModel<string>
    {
        public MxaProperty<TimeSlotVm> TimeSlot { get; } = new MxaProperty<TimeSlotVm>();

        public TimeSlotViewModel()
        {
            WhenActivated(
                disposables =>
                {
                    AppState.Conference.SubscribeUi(
                        conf =>
                        {
                            TimeSlot.Value =
                                conf.TimeSlots.Where(ts => ts.TimeSlotId == Parameter)
                                    .Select(ts => TimeSlotVm.FromTimeSlot(ts, conf))
                                    .FirstOrDefault() ?? new TimeSlotVm
                                {
                                    TimeSlotId = "",
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