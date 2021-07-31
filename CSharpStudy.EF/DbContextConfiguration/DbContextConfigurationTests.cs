using System;
using CSharpStudy.EF.Helpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CSharpStudy.EF.DbContextConfiguration
{
    [TestFixture]
    public class DbContextConfigurationTests
    {
        public class Person
        {
            public int Id { get; set; }

            public string Firstname { get; set; }

            public string LastName { get; set; }
        }

        public class PersonsContext : DbContext
        {
            private readonly string _connectionString;

            public PersonsContext(string connectionString)
            {
                _connectionString = connectionString;
            }

            public PersonsContext(DbContextOptions<PersonsContext> options) : base(options)
            {
            }

            public DbSet<Person> Persons { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (_connectionString is not null)
                {
                    optionsBuilder.UseNpgsql(_connectionString);
                }
            }
        }

        [SetUp]
        public void SetUp()
        {
            using var db = new PersonsContext(Constants.ConnectionString);
            db.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            using var db = new PersonsContext(Constants.ConnectionString);
            db.Persons.Clear();
            db.SaveChanges();
        }

        [Test]
        public void Run()
        {
            using (var db = new PersonsContext(Constants.ConnectionString))
            {
                var person = new Person
                {
                    Firstname = "Travis",
                    LastName = "Howe"
                };
                db.Persons.Add(person);
                db.SaveChanges();
            }

            var options = new DbContextOptionsBuilder<PersonsContext>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine)
                .UseNpgsql(Constants.ConnectionString)
                .Options;

            using (var db = new PersonsContext(options))
            {
                var customer = new Person
                {
                    Firstname = "Edward",
                    LastName = "Chester"
                };
                db.Persons.Add(customer);
                db.SaveChanges();
            }
        }
    }
}