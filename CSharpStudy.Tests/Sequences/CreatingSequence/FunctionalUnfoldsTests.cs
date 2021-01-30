using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading;
using NUnit.Framework;

namespace CSharpStudy.Tests.Sequences.CreatingSequence
{
    [TestFixture]
    public class FunctionalUnfoldsTests
    {
        [Test]
        public void ObservableRangeTest()
        {
            var observableRange = Observable.Range(10, 5);

            observableRange.Subscribe(x => Trace.WriteLine(x));
        }

        [Test]
        public void ObservableGenerateTest()
        {
            var observableRange = Range(10, 5);

            observableRange.Subscribe(x => Trace.WriteLine(x));
        }

        private static IObservable<int> Range(int start, int count)
        {
            var max = start + count;
            return Observable.Generate(start,
                x => x < max,
                x => x + 1,
                x => x);
        }

        [Test]
        public void ObservableIntervalTest()
        {
            var observableInterval = Observable.Interval(TimeSpan.FromMilliseconds(250));
            
            observableInterval.Subscribe(x => Trace.WriteLine(x));
            
            Thread.Sleep(1000);
        }

        [Test]
        public void ObservableTimerTest()
        {
            var observableTimer = Observable.Timer(TimeSpan.FromSeconds(1));
            
            observableTimer.Subscribe(x => Trace.WriteLine(x), () => Trace.WriteLine("Completed"));
            
            Thread.Sleep(1000);
        }
        
        [Test]
        public void ObservableTimerPeriodTest()
        {
            var observableTimer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(500));
            
            observableTimer.Subscribe(x => Trace.WriteLine(x), () => Trace.WriteLine("Completed"));
            
            Thread.Sleep(5000);
        }
    }
}