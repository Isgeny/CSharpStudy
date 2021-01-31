using System;
using NUnit.Framework;

namespace CSharpStudy.Tests.CSharp
{
    [TestFixture]
    public class InitPropertiesTests
    {
        public class TestData
        {
            private readonly Action _property1Invoked;
            private readonly Action _property2Invoked;
            private readonly int _property1;
            private readonly int _property2;

            public TestData(Action property1Invoked, Action property2Invoked)
            {
                _property1Invoked = property1Invoked;
                _property2Invoked = property2Invoked;
            }

            public int Property1
            {
                get => _property1;
                init
                {
                    _property1 = value;
                    _property1Invoked?.Invoke();
                }
            }

            public int Property2
            {
                get => _property2;
                init
                {
                    _property2 = value;
                    _property2Invoked?.Invoke();
                }
            }
        }

        [Test]
        public void InitPropertiesTest()
        {
            var counter1 = 0;
            var counter2 = 0;

            var testData = new TestData(() => counter1++, () => counter2++)
            {
                Property1 = 1
            };

            Assert.AreEqual(1, counter1);
            Assert.AreEqual(0, counter2);
        }
    }
}