using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NidcApp.MxaUi
{
    public static class ExtensionMethods
    {
        public static T Value<T>(this ReplaySubject<T> subject)
        {
            return subject.Latest().First();
        }

        public static void DisposeWith(this IDisposable disposable, CompositeDisposable disposables)
        {
            disposables.Add(disposable);
        }

        public static IDisposable SubscribeUi<T>(this IObservable<T> observable, Action<T> onNext)
        {
            return observable.ObserveOn(SynchronizationContext.Current).Subscribe(onNext);
        }

        public static IDisposable Subscribe<T>(this Task<T> task, Action<T> onNext)
        {
            return task.ToObservable().Subscribe(onNext);
        }

        public static IObservable<(T1 val1, T2 val2)> ObserveTogether<T1, T2>(
            this IObservable<T1> o1,
            IObservable<T2> o2)
        {
            return o1.CombineLatest(o2, (val1, val2) => (val1, val2));
        }

        public static IDisposable WhenTextChanged(this Entry ctl, Action<Entry, TextChangedEventArgs> action)
        {
            return Observable
                .FromEventPattern<TextChangedEventArgs>(h => ctl.TextChanged += h, h => ctl.TextChanged -= h)
                .Subscribe(args => action((Entry)args.Sender, args.EventArgs));
        }
    }
}