using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using NUnit.Framework;

namespace RxStudy.Sequences.CreatingSequence
{
    [TestFixture]
    public class SimpleFactoryMethodsTests
    {
        [Test]
        public void ObservableReturnTest()
        {
            var observable = Observable.Return("Value");

            // same
            // var subject = new ReplaySubject<string>();
            // subject.OnNext("Value");
            // subject.OnCompleted();

            SubscribeConsole(observable);
        }

        [Test]
        public void ObservableEmptyTest()
        {
            var emptyObservable = Observable.Empty<string>();

            // same
            // var subject = new ReplaySubject<string>();
            // subject.OnCompleted();

            SubscribeConsole(emptyObservable);
        }

        [Test]
        public void ObservableNeverTest()
        {
            var neverObservable = Observable.Never<string>();

            // same
            // var subject = new Subject<string>();

            SubscribeConsole(neverObservable);
        }

        [Test]
        public void ObservableThrowsTest()
        {
            var throwObservable = Observable.Throw<string>(new Exception());

            // var subject = new ReplaySubject<string>(); 
            // subject.OnError(new Exception());

            SubscribeConsole(throwObservable);
        }

        [Test]
        public void ObservableBlockingTest()
        {
            var observable = BlockingMethod();

            SubscribeConsole(observable);
        }

        [Test]
        public void ObservableNonBlockingTest()
        {
            var observable = NonBlockingMethod();

            SubscribeConsole(observable);
        }

        [Test]
        public void ObservableMyReturn()
        {
            var observable = MyReturn("Value");

            SubscribeConsole(observable);
        }

        [Test]
        public void ObservableMyEmpty()
        {
            var emptyObservable = MyEmpty<string>();

            SubscribeConsole(emptyObservable);
        }

        [Test]
        public void ObservableMyNever()
        {
            var neverObservable = MyNever<string>();

            SubscribeConsole(neverObservable);
        }

        [Test]
        public void ObservableMyThrows()
        {
            var throwsObservable = MyThrows<string>(new Exception());

            SubscribeConsole(throwsObservable);
        }

        private IObservable<string> BlockingMethod()
        {
            var subject = new ReplaySubject<string>();
            subject.OnNext("a");
            subject.OnNext("b");
            Thread.Sleep(1000);
            subject.OnCompleted();
            return subject;
        }

        private IObservable<string> NonBlockingMethod()
        {
            return Observable.Create<string>(x =>
            {
                x.OnNext("a");
                x.OnNext("b");
                Thread.Sleep(1000);
                x.OnCompleted();
                return () => Trace.WriteLine("Disposed");
                // or
                // return Disposable.Create(() => Trace.WriteLine("Disposed"));
            });
        }

        private static IObservable<T> MyReturn<T>(T value)
        {
            return Observable.Create<T>(x =>
            {
                x.OnNext(value);
                x.OnCompleted();
                return Disposable.Empty;
            });
        }

        private static IObservable<T> MyEmpty<T>()
        {
            return Observable.Create<T>(x =>
            {
                x.OnCompleted();
                return Disposable.Empty;
            });
        }

        private static IObservable<T> MyNever<T>()
        {
            return Observable.Create<T>(x => Disposable.Empty);
        }

        private static IObservable<T> MyThrows<T>(Exception exception)
        {
            return Observable.Create<T>(x =>
            {
                x.OnError(exception);
                return Disposable.Empty;
            });
        }

        private static void SubscribeConsole<T>(IObservable<T> observable)
        {
            observable.Subscribe(
                x => Trace.WriteLine(x),
                x => Trace.WriteLine($"Error: {x}"),
                () => Trace.WriteLine("Completed"));
        }
    }
}