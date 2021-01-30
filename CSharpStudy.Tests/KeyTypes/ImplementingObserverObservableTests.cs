using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using NUnit.Framework;

namespace CSharpStudy.Tests.KeyTypes
{
    [TestFixture]
    public class ImplementingObserverObservableTests
    {
        [Test]
        public void ImplementingObserverObservableTest()
        {
            var sequenceOfNumbers = new SequenceOfNumbers();
            var consoleObserver = new ConsoleObserver<int>();
            sequenceOfNumbers.Subscribe(consoleObserver);
        }
    }

    public class ConsoleObserver<T> : IObserver<T>
    {
        public void OnNext(T value)
        {
            Trace.WriteLine($"Received value {value}");
        }

        public void OnError(Exception error)
        {
            Trace.WriteLine($"Sequence faulted with {error}");
        }

        public void OnCompleted()
        {
            Trace.WriteLine("Sequence terminated");
        }
    }

    public class SequenceOfNumbers : IObservable<int>
    {
        public IDisposable Subscribe(IObserver<int> observer)
        {
            observer.OnNext(1);
            observer.OnNext(2);
            observer.OnNext(3);
            observer.OnCompleted();
            return Disposable.Empty;
        }
    }
}