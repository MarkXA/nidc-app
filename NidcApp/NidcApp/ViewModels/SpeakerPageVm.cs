using System.Linq;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class SpeakerPageVm : BaseViewModel<string>
    {
        public MxaProperty<SpeakerVm> Speaker { get; } = new MxaProperty<SpeakerVm>();

        public SpeakerPageVm()
        {
            WhenActivated(
                disposables =>
                {
                    AppState.Conference.SubscribeUi(
                        conf =>
                        {
                            Speaker.Value = conf.speakers.Where(ts => ts.id == Parameter)
                                .Select(ts => SpeakerVm.FromSpeaker(ts, conf))
                                .FirstOrDefault();
                        });
                });
        }
    }
}