using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class AgendaPageVm : BaseViewModel<Unit>
    {
        public MxaProperty<List<TimeslotVm>> Timeslots { get; } = new List<TimeslotVm>();

        public AgendaPageVm()
        {
            WhenActivated(
                disposables =>
                {
                    AppState.Conference.SubscribeUi(
                        conf =>
                        {
                            Timeslots.Value = conf.timeslots.Select(ts => TimeslotVm.FromTimeslot(ts, conf)).ToList();
                        });
                });
        }
    }
}