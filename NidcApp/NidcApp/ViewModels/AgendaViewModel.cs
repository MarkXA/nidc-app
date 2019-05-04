using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class AgendaViewModel : BaseViewModel<Unit>
    {
        public MxaProperty<List<TimeSlotVm>> TimeSlots { get; } = new List<TimeSlotVm>();

        public AgendaViewModel()
        {
            WhenActivated(
                disposables =>
                {
                    AppState.Conference.SubscribeUi(
                        conf =>
                        {
                            TimeSlots.Value = conf.TimeSlots.Select(ts => TimeSlotVm.FromTimeSlot(ts, conf)).ToList();
                        });
                });
        }
    }
}