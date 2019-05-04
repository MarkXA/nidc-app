using System.Linq;
using NidcApp.MxaUi;

namespace NidcApp.ViewModels
{
    public class SpeakerViewModel : BaseViewModel<string>
    {
        public MxaProperty<SpeakerVm> Speaker { get; } = new MxaProperty<SpeakerVm>();

        public SpeakerViewModel()
        {
            WhenActivated(
                disposables =>
                {
                    AppState.Conference.SubscribeUi(
                        conf =>
                        {
                            Speaker.Value = conf.Speakers.Where(ts => ts.SpeakerId == Parameter)
                                .Select(ts => SpeakerVm.FromSpeaker(ts, conf))
                                .FirstOrDefault();
                        });
                });
        }
    }
}