using System;
using System.ComponentModel;
using System.Reactive.Subjects;

namespace NidcApp.MxaUi
{
    public class MxaProperty<T> : SubjectBase<T>, INotifyPropertyChanged
    {
        private readonly ReplaySubject<T> baseSubject = new ReplaySubject<T>(1);

        private T _Value;

        public T Value
        {
            get => _Value;
            set => OnNext(value);
        }

        public static implicit operator MxaProperty<T>(T value)
        {
            return new MxaProperty<T> {Value = value};
        }

        public static implicit operator T(MxaProperty<T> value)
        {
            return value.Value;
        }

        public override void Dispose()
        {
            baseSubject.Dispose();
        }

        public override void OnCompleted()
        {
            baseSubject.OnCompleted();
        }

        public override void OnError(Exception error)
        {
            baseSubject.OnError(error);
        }

        public override void OnNext(T value)
        {
            if (_Value == null && value == null)
                return;
            if (_Value?.Equals(value) ?? false)
                return;
            _Value = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            baseSubject.OnNext(value);
        }

        public override IDisposable Subscribe(IObserver<T> observer)
        {
            return baseSubject.Subscribe(observer);
        }

        public override bool HasObservers => baseSubject.HasObservers;

        public override bool IsDisposed => baseSubject.IsDisposed;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}