using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class LightningPageVm : BaseViewModel<Unit>
    {
        public MxaProperty<List<SessionVm>> Sessions { get; } = new MxaProperty<List<SessionVm>>();

        public LightningPageVm()
        {
            WhenActivated(
                () => new[]
                {
                    AppState.Conference.SubscribeUi(
                        conf =>
                        {
                            Sessions.Value = conf.sessions.Where(s => s.lightning)
                                .OrderBy(s => s.order)
                                .Select(
                                    (s, n) => SessionVm.FromSession(s, conf, conf.lightningslots[n].title))
                                .ToList();
                        })
                });
        }
    }
}