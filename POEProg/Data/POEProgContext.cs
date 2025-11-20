using Microsoft.EntityFrameworkCore;
using POEProg.Models;

namespace POEProg.Data
{
    public class POEProgContext : DbContext
    {
        public POEProgContext(DbContextOptions<POEProgContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        // Seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "HR",
                    LastName = "Admin",
                    Email = "hr@university.edu",
                    Password = "Password123!",
                    HourlyRate = 0,
                    Role = Role.HR
                },
                new User
                {
                    Id = 2,
                    FirstName = "Coord",
                    LastName = "User",
                    Email = "coordinator@university.edu",
                    Password = "Password123!",
                    HourlyRate = 0,
                    Role = Role.Coordinator
                },
                new User
                {
                    Id = 3,
                    FirstName = "Manager",
                    LastName = "User",
                    Email = "manager@university.edu",
                    Password = "Password123!",
                    HourlyRate = 0,
                    Role = Role.Manager
                }
            );
        }
    }
}
