namespace CSharpStudy.EF
{
    internal class Program
    {
        private static void Main()
        {
            using var db = new ApplicationContext();

            var employee = new Employee
            {
                Firstname = "Travis",
                LastName = "Howe"
            };
            db.Employees.Add(employee);

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