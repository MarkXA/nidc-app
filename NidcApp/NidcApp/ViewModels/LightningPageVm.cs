using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class LightningPageVm : BaseViewModel<Unit>
    {
        public MxaProperty<string> IntroText { get; } = "";
        public MxaProperty<List<SessionVm>> Sessions { get; } = new MxaProperty<List<SessionVm>>();

        public LightningPageVm()
        {
            WhenActivated(
                disposables =>
                {
                    AppState.Conference.SubscribeUi(
                        conf =>
                        {
                            IntroText.Value = conf.lightningIntro;
                            Sessions.Value = conf.sessions.Where(s => s.lightning)
                                .OrderBy(s => s.order)
                                .Select(s => SessionVm.FromSession(s, conf))
                                .ToList();
                        });
                });
        }
    }
}