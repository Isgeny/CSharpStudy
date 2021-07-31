using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpStudy.EF.Helpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CSharpStudy.EF.NotMappedCustomProperty
{
    [TestFixture]
    public class NotMappedCustomPropertyTests
    {
        public class Group
        {
            public int Id { get; set; }

            public string Name { get; set; }

            [NotMapped]
            public bool HasUsers { get; set; }
        }

        public class GroupSatellite : Group
        {
            public List<Link> Links { get; set; }
        }

        public class User
        {
            public int Id { get; set; }

            public string Name { get; set; }

            [NotMapped]
            public bool InAnyGroup { get; set; }
        }

        public class UserSatellite : User
        {
            public List<Link> Links { get; set; }
        }

        public class Link
        {
            public GroupSatellite Group { get; set; }

            public int GroupId { get; set; }

            public UserSatellite User { get; set; }

            public int UserId { get; set; }
        }

        public class AppProfile : Profile
        {
            public AppProfile()
            {
                CreateMap<Group, GroupSatellite>()
                    .ReverseMap()
                    .ForMember(x => x.HasUsers, x => x.MapFrom(y => y.Links.Any()));

                CreateMap<User, UserSatellite>()
                    .ReverseMap()
                    .ForMember(x => x.InAnyGroup, x => x.MapFrom(y => y.Links.Any()));
            }
        }

        public class AppContext : DbContext
        {
            public DbSet<GroupSatellite> Groups { get; set; }

            public DbSet<UserSatellite> Users { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder
                    .UseNpgsql(Constants.ConnectionString)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    .LogTo(x => Trace.WriteLine(x));
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Link>()
                    .HasKey(x => new {x.GroupId, x.UserId});

                modelBuilder.Entity<Link>()
                    .HasOne(x => x.Group)
                    .WithMany(x => x.Links)
                    .HasForeignKey(x => x.GroupId);

                modelBuilder.Entity<Link>()
                    .HasOne(x => x.User)
                    .WithMany(x => x.Links)
                    .HasForeignKey(x => x.UserId);
            }
        }

        [SetUp]
        public void SetUp()
        {
            using var db = new AppContext();
            db.Database.EnsureCreated();

            var emptyGroup = new GroupSatellite {Name = "Empty"};
            var withUsersGroup = new GroupSatellite {Name = "WithUsers"};
            db.Groups.AddRange(emptyGroup, withUsersGroup);

            var travis = new UserSatellite {Name = "Travis"};
            var edward = new UserSatellite {Name = "Edward"};
            db.Users.AddRange(travis, edward);

            withUsersGroup.Links = new List<Link>
            {
                new()
                {
                    Group = withUsersGroup,
                    User = travis
                }
            };

            db.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            using var db = new AppContext();
            db.Groups.Clear();
            db.Users.Clear();
            db.SaveChanges();
        }

        [Test]
        public void Test()
        {
            using var db = new AppContext();

            var mapperConfiguration = new MapperConfiguration(x => x.AddProfile<AppProfile>());
            var mapper = mapperConfiguration.CreateMapper();

            var groups = db.Set<GroupSatellite>()
                .ProjectTo<Group>(mapper.ConfigurationProvider)
                .ToList();

            var users = db.Set<UserSatellite>()
                .ProjectTo<User>(mapper.ConfigurationProvider)
                .ToList();
        }
    }
}