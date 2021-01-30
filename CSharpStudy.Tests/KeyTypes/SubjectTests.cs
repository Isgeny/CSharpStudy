using System;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Threading;
using NUnit.Framework;

namespace CSharpStudy.Tests.KeyTypes
{
    [TestFixture]
    public class SubjectTests
    {
        [Test]
        public void SubjectTest()
        {
            var subject = new Subject<string>();
            subject.OnNext("a");
            SubscribeConsole(subject);
            subject.OnNext("b");
            subject.OnNext("c");
        }

        [Test]
        public void ReplaySubjectTest()
        {
            var subject = new ReplaySubject<string>();
            subject.OnNext("a");
            SubscribeConsole(subject);
            subject.OnNext("b");
            subject.OnNext("c");
        }

        [Test]
        public void ReplaySubjectWithBufferSizeTest()
        {
            var subject = new ReplaySubject<string>(2);
            subject.OnNext("a");
            subject.OnNext("c");
            subject.OnNext("d");
            SubscribeConsole(subject);
            subject.OnNext("e");
        }

        [Test]
        public void ReplaySubjectWithWindowTest()
        {
            var subject = new ReplaySubject<string>(TimeSpan.FromMilliseconds(150));
            subject.OnNext("a");
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            subject.OnNext("b");
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            subject.OnNext("c");
            SubscribeConsole(subject);
            subject.OnNext("d");
        }

        [Test]
        public void BehaviorSubjectInitialTest()
        {
            var subject = new BehaviorSubject<string>("a");
            SubscribeConsole(subject);
        }

        [Test]
        public void BehaviorSubjectInitialNotPublishedTest()
        {
            var subject = new BehaviorSubject<string>("a");
            subject.OnNext("b");
            SubscribeConsole(subject);
            subject.OnNext("c");
            subject.OnNext("d");
        }

        [Test]
        public void BehaviorSubjectNothingPublishedTest()
        {
            var subject = new BehaviorSubject<string>("a");
            subject.OnNext("b");
            subject.OnNext("c");
            subject.OnCompleted();
            SubscribeConsole(subject);
        }

        [Test]
        public void AsyncSubjectNothingPublishedTest()
        {
            var subject = new AsyncSubject<string>();
            subject.OnNext("a");
            subject.OnNext("b");
            SubscribeConsole(subject);
            subject.OnNext("c");
        }

        [Test]
        public void AsyncSubjectLastValuePublishedTest()
        {
            var subject = new AsyncSubject<string>();
            subject.OnNext("a");
            subject.OnNext("b");
            SubscribeConsole(subject);
            subject.OnNext("c");
            subject.OnCompleted();
        }

        [Test]
        public void SubjectNotPublishedAfterCompleteTest()
        {
            var subject = new Subject<string>();
            subject.OnNext("a");
            SubscribeConsole(subject);
            subject.OnNext("b");
            subject.OnCompleted();
            subject.OnNext("c");
        }

        [Test]
        public void SubjectNotPublishedAfterErrorTest()
        {
            var subject = new Subject<string>();
            subject.OnNext("a");
            SubscribeConsole(subject);
            subject.OnNext("b");

            try
            {
                subject.OnError(new Exception());
            }
            catch (Exception)
            {
                // ignored
            }

            subject.OnNext("c");
        }

        [Test]
        public void SubjectOnErrorHandlingTest()
        {
            var subject = new Subject<int>();

            subject.Subscribe(x => Trace.WriteLine(x), x => Trace.WriteLine(x));

            subject.OnNext(1);

            subject.OnError(new Exception());
        }

        private static void SubscribeConsole(IObservable<string> sequence)
        {
            sequence.Subscribe(x => Trace.WriteLine(x));
        }
    }
}