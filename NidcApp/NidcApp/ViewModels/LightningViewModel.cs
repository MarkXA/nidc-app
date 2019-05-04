using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class LightningViewModel : BaseViewModel<Unit>
    {
        public MxaProperty<string> IntroText { get; } = "";
        public MxaProperty<List<SessionVm>> Sessions { get; } = new MxaProperty<List<SessionVm>>();

        public LightningViewModel()
        {
            WhenActivated(
                disposables =>
                {
                    AppState.Conference.SubscribeUi(
                        conf =>
                        {
                            IntroText.Value = conf.LightningIntro;
                            Sessions.Value = conf.Sessions.Where(s => s.Lightning)
                                .OrderBy(s => s.Order)
                                .Select(s => SessionVm.FromSession(s, conf))
                                .ToList();
                        });
                });
        }
    }
}