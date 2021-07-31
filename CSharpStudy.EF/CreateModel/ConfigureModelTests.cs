using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpStudy.EF.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NUnit.Framework;

namespace CSharpStudy.EF.CreateModel
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Blog Blog { get; set; }
    }

    public class AuditEntry
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Action { get; set; }
    }

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(x => x.Url).IsRequired();
        }
    }

    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Constants.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1
            // modelBuilder.Entity<Blog>()
            //     .Property(x => x.Url)
            //     .IsRequired();

            // 2
            // new BlogConfiguration().Configure(modelBuilder.Entity<Blog>());

            // 3
            // modelBuilder.ApplyConfiguration(new BlogConfiguration());

            // 4 all types implementing IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }

    [TestFixture]
    public class ConfigureModelTests
    {
        [SetUp]
        public void SetUp()
        {
            using var db = new BlogContext();
            db.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            using var db = new BlogContext();
            db.Blogs.Clear();
            db.SaveChanges();
        }

        [Test]
        public void Test()
        {
            using var db = new BlogContext();

            var blog = new Blog
            {
                Url = "TestUrl"
            };

            db.Blogs.Add(blog);
            db.SaveChanges();
        }
    }
}