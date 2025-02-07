using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace InfrastructureLayer
{
    public class TutoringKidDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public TutoringKidDbContext(DbContextOptions<TutoringKidDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // OPTIONAL: Remove this if `Program.cs` handles configuration
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        public DbSet<DomainLayer.Entities.User> Users { get; set; }
        public DbSet<TutorProfile> TutorProfiles { get; set; }
        public DbSet<TransactionHistory> Transactions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCourse> OrderCourses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<BoughtCourse> BoughtCourses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }


        public string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            return configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Explicitly define Schedule → User relationship
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.CreatedByUser)    // Navigation Property
                .WithMany(u => u.Schedules)      // Reverse Navigation
                .HasForeignKey(s => s.CreatedBy) // Explicit Foreign Key
                .IsRequired()                    // Ensure it's required
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete
            // CourseCategory Relationship (Many-to-Many)
            modelBuilder.Entity<CourseCategory>()
                .HasOne(cc => cc.Course)
                .WithMany(c => c.CourseCategories)
                .HasForeignKey(cc => cc.CourseId);

            modelBuilder.Entity<CourseCategory>()
                .HasOne(cc => cc.Category)
                .WithMany(c => c.CourseCategories)
                .HasForeignKey(cc => cc.CategoryId);

            // Review Relationship
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CourseId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.CreatedByUser)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.CreatedBy);
        }
    }
}
