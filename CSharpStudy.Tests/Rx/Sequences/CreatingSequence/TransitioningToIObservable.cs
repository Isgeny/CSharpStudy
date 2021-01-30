using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSharpStudy.Tests.Rx.Sequences.CreatingSequence
{
    [TestFixture]
    public class TransitioningToIObservable
    {
        [Test]
        public void ObservableStartVoidTest()
        {
            var observable = Observable.Start(() =>
            {
                Trace.WriteLine("Long running operation");

                for (var i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Trace.Write(".");
                }

                Trace.WriteLine("");
            });

            observable.Subscribe(x => Trace.WriteLine("Unit published"), () => Trace.WriteLine("Completed"));

            Thread.Sleep(2000);
        }

        [Test]
        public void ObservableStartResultTest()
        {
            var observable = Observable.Start(() =>
            {
                Trace.WriteLine("Long running operation");

                var sum = 0;
                for (var i = 0; i < 10; i++)
                {
                    sum += i;
                    Thread.Sleep(100);
                    Trace.Write(".");
                }

                Trace.WriteLine("");
                return sum;
            });

            observable.Subscribe(x => Trace.WriteLine($"Result: {x}"), () => Trace.WriteLine("Completed"));

            Thread.Sleep(2000);
        }

        public event EventHandler Event;

        [Test]
        public void ObservableFromEventPatternBaseEventArgs()
        {
            var observable = Observable.FromEventPattern(
                x => Event += x,
                x => Event -= x);

            var subscriber = observable.Subscribe(
                x => Trace.WriteLine($"Sender: {x.Sender}, EventArgs: {x.EventArgs}"),
                () => Trace.WriteLine("Never completed?"));

            Event?.Invoke(this, new EventArgs());

            subscriber.Dispose();
        }

        public event EventHandler<DerivedEventArgs> EventWithData;

        [Test]
        public void ObservableFromEventPatternDerivedEventArgs()
        {
            var observable = Observable.FromEventPattern<DerivedEventArgs>(
                x => EventWithData += x,
                x => EventWithData -= x);

            var subscriber = observable.Subscribe(
                x => Trace.WriteLine($"Sender: {x.Sender}, EventArgs: {x.EventArgs.Data}"),
                () => Trace.WriteLine("Never completed?"));

            EventWithData?.Invoke(this, new DerivedEventArgs {Data = "Some data"});

            subscriber.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [Test]
        public void ObservableFromEventPatternAnotherHandler()
        {
            var observable = Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                x => PropertyChanged += x,
                x => PropertyChanged -= x);

            var subscriber = observable.Subscribe(
                x => Trace.WriteLine($"Sender: {x.Sender}, EventArgs: {x.EventArgs.PropertyName}"),
                () => Trace.WriteLine("Never completed?"));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Data"));

            subscriber.Dispose();
        }

        [Test]
        public void ObservableFromTask()
        {
            var task = Task.Run(() => "Data");

            var source = task.ToObservable();

            source.Subscribe(x => Trace.WriteLine(x), () => Trace.WriteLine("Completed"));
        }
    }

    public class DerivedEventArgs : EventArgs
    {
        public string Data { get; set; }
    }
}