using System;
using System.Reactive.Subjects;

namespace NidcApp.MxaUi
{
    public class MxaSignal<T> : SubjectBase<T>
    {
        private readonly Subject<T> baseSubject = new Subject<T>();

        public void Signal(T value = default)
        {
            OnNext(value);
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
            baseSubject.OnNext(value);
        }

        public override IDisposable Subscribe(IObserver<T> observer)
        {
            return baseSubject.Subscribe(observer);
        }

        public override bool HasObservers => baseSubject.HasObservers;

        public override bool IsDisposed => baseSubject.IsDisposed;
    }
}