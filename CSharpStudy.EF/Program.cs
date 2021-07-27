using System;
using Microsoft.EntityFrameworkCore;

namespace CSharpStudy.EF
{
    internal class Program
    {
        private static void Main()
        {
            const string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";
            using (var db = new ApplicationContext(connectionString))
            {
                db.Database.EnsureCreated();
                var employee = new Employee
                {
                    Firstname = "Travis",
                    LastName = "Howe"
                };
                db.Employees.Add(employee);
                db.SaveChanges();
            }

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine)
                .UseNpgsql(connectionString)
                .Options;

            using (var db = new ApplicationContext(options))
            {
                var customer = new Customer
                {
                    Firstname = "Edward",
                    LastName = "Chester"
                };
                db.Customers.Add(customer);
                db.SaveChanges();
            }
        }
    }
}