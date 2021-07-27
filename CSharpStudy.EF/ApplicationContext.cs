using Microsoft.EntityFrameworkCore;

namespace CSharpStudy.EF
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString is not null)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }
    }
}