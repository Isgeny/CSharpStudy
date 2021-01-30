using System.Linq;
using LiteDB;
using NUnit.Framework;

namespace CSharpStudy.Tests.LiteDb.HelloWorld
{
    [TestFixture]
    public class HelloWorld
    {
        public class Customer
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string[] Phones { get; set; }

            public bool IsActive { get; set; }
        }

        public class HelloLiteDb
        {
            [Test]
            public void Test()
            {
                using var db = new LiteDatabase("hello.db");

                var customers = db.GetCollection<Customer>();

                var customer = new Customer
                {
                    Name = "John Doe",
                    Phones = new[] {"8000-0000", "9000-0000"},
                    IsActive = true
                };
                customers.Insert(customer);

                customer.Name = "Jane Doe";
                customers.Update(customer);

                customers.EnsureIndex(x => x.Name);

                var results = customers.Query()
                    .Where(x => x.Name.StartsWith("J"))
                    .OrderBy(x => x.Name)
                    .Select(x => new {x.Name, NameUpper = x.Name.ToUpper()})
                    .Limit(10)
                    .ToList();

                customers.EnsureIndex(x => x.Phones);

                var r = customers.FindOne(x => x.Phones.Contains("8888-5555"));
            }
        }
    }
}