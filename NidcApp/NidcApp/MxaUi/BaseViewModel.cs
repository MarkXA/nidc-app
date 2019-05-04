using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using Xamarin.Forms;

namespace NidcApp.MxaUi
{
    public abstract class BaseViewModel<TParameter> : INotifyPropertyChanged
    {
        private bool isActive;
        private readonly CompositeDisposable disposables;
        private readonly bool ownDisposables;
        private Action<CompositeDisposable> activateAction;
        private Func<IEnumerable<IDisposable>> getActivateDisposables;

        public INavigation Navigation;
        public TParameter Parameter;

        protected BaseViewModel() : this(new CompositeDisposable(), true) { }

        protected BaseViewModel(CompositeDisposable disposables) : this(disposables, false) { }

        private BaseViewModel(CompositeDisposable disposables, bool ownDisposables)
        {
            this.disposables = disposables;
            this.ownDisposables = ownDisposables;
        }

        protected void WhenActivated(Action<CompositeDisposable> action)
        {
            activateAction = action;
        }

        protected void WhenActivated(Func<IEnumerable<IDisposable>> getDisposables)
        {
            getActivateDisposables = getDisposables;
        }

        protected void WhenActivated(Func<IEnumerable<IDisposable>> getDisposables, Action<CompositeDisposable> action)
        {
            WhenActivated(getDisposables);
            WhenActivated(action);
        }

        public void Activate()
        {
            if (isActive)
                return;
            isActive = true;

            activateAction?.Invoke(disposables);
            if (getActivateDisposables != null)
                foreach (var disposable in getActivateDisposables())
                    disposables.Add(disposable);
        }

        public void Deactivate()
        {
            if (!isActive)
                return;
            isActive = false;

            if (ownDisposables)
                disposables?.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}