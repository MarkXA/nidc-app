using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using Xamarin.Forms;

namespace NidcApp.MxaUi
{
    public abstract class BaseContentPage<TViewModel, TParameter> : ContentPage
        where TViewModel : BaseViewModel<TParameter>, new()
    {
        protected TViewModel ViewModel
        {
            get => BindingContext as TViewModel;
            private set => BindingContext = value;
        }

        private readonly CompositeDisposable disposables = new CompositeDisposable();

        protected BaseContentPage(TParameter param)
        {
            ViewModel = new TViewModel {Parameter = param, Navigation = Navigation};

            Appearing += (sender, args) => { ViewModel?.Activate(); };
            Disappearing += (sender, args) =>
            {
                disposables.Clear();
                ViewModel?.Deactivate();
            };
        }

        protected BaseContentPage() : this(default(TParameter)) { }

        protected IDisposable WhenChanged<T1>(IObservable<T1> o1, Action<T1> action)
        {
            return o1.ObserveOn(SynchronizationContext.Current).Subscribe(action);
        }

        protected IDisposable WhenChanged<T1, T2>(IObservable<T1> o1, IObservable<T2> o2, Action<T1, T2> action)
        {
            return o1.CombineLatest(o2, (v1, v2) => new {v1, v2})
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(v => action(v.v1, v.v2));
        }

        protected void WhenActivated(Action<CompositeDisposable> action)
        {
            Appearing += (sender, args) => { action?.Invoke(disposables); };
        }

        protected void WhenActivated(Func<IEnumerable<IDisposable>> getDisposables)
        {
            Appearing += (sender, args) =>
            {
                foreach (var disposable in getDisposables())
                    disposables.Add(disposable);
            };
        }

        protected void WhenActivated(Func<IEnumerable<IDisposable>> getDisposables, Action<CompositeDisposable> action)
        {
            WhenActivated(getDisposables);
            WhenActivated(action);
        }
    }
}