﻿using System;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CSharpStudy.Tests.NewtonsoftJson
{
    [TestFixture]
    public class NewtonsoftJsonTests
    {
        public class TestData
        {
            public TimeSpan TimeSpan { get; set; }

            public TimeSpan? NullableTimeSpan { get; set; }
        }

        [Test]
        public void Serialize()
        {
            var testData = new TestData
            {
                TimeSpan = new TimeSpan(1000, 13, 25, 56, 1)
            };

            var testDataJson = JsonConvert.SerializeObject(testData);

            File.WriteAllText("TestData.json", testDataJson);
        }
    }
}