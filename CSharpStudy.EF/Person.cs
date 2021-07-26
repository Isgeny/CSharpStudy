using System;

namespace CSharpStudy.EF
{
    public abstract class Person
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string LastName { get; set; }
    }

    public class Customer : Person
    {
    }

    public class Employee : Person
    {
    }

    // public class Customer
    // {
    //     public Guid Id { get; set; }
    //
    //     public string Firstname { get; set; }
    //
    //     public string LastName { get; set; }
    // }
    //
    // public class Employee
    // {
    //     public Guid Id { get; set; }
    //
    //     public string Firstname { get; set; }
    //
    //     public string LastName { get; set; }
    // }
}