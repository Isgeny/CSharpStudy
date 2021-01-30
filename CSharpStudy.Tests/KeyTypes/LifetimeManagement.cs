using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using NUnit.Framework;

namespace CSharpStudy.Tests.KeyTypes
{
    [TestFixture]
    public class LifetimeManagement
    {
        [Test]
        public void UnsubscribeTest()
        {
            var subject = new Subject<int>();

            var firstSubscribe = subject.Subscribe(x => Trace.WriteLine($"1st subscribe: {x}"));
            var secondSubscribe = subject.Subscribe(x => Trace.WriteLine($"2nd subscribe: {x}"));

            subject.OnNext(1);
            subject.OnNext(2);

            firstSubscribe.Dispose();

            subject.OnNext(3);
            subject.OnNext(4);
        }

        [Test]
        public void UnsubscribeTwiceNothingHappenTest()
        {
            var subject = new Subject<int>();

            var firstSubscribe = subject.Subscribe(x => Trace.WriteLine($"1st subscribe: {x}"));
            var secondSubscribe = subject.Subscribe(x => Trace.WriteLine($"2nd subscribe: {x}"));

            subject.OnNext(1);
            subject.OnNext(2);

            firstSubscribe.Dispose();

            subject.OnNext(3);
            subject.OnNext(4);

            firstSubscribe.Dispose();

            subject.OnNext(5);
            subject.OnNext(6);
        }

        [Test]
        public void DisposableTest()
        {
            var emptyDisposable = Disposable.Empty;
            emptyDisposable.Dispose();

            var disposable = Disposable.Create(() => Trace.WriteLine("Disposed"));
            disposable.Dispose();
            disposable.Dispose();
        }
    }
}