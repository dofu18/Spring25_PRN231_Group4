using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using static DomainLayer.Enums.GeneralEnum;

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

        //public static string GetConnectionString(string connectionStringName)
        //{
        //    var config = new ConfigurationBuilder()
        //        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    string connectionString = config.GetConnectionString(connectionStringName);
        //    return connectionString;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseNpgsql(GetConnectionString("DefaultConnection"));

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                }
              ((BaseEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<User>(u =>
            {
                u.HasKey(x => x.Id);
                u.HasIndex(x => x.Email).IsUnique();
                u.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                u.Property(x => x.LastName).IsRequired().HasMaxLength(100);
                u.Property(x => x.Phone).IsRequired(false).HasMaxLength(20);
                u.Property(x => x.ProfileUrl).IsRequired(false).HasMaxLength(1000);
                u.Property(x => x.Credits).IsRequired();
                u.Property(x => x.Meta).IsRequired(false).HasMaxLength(1000);
                u.Property(x => x.Role).IsRequired().HasDefaultValue(UserRoleEnum.Parent);
                u.Property(x => x.UserName).IsRequired(false).HasMaxLength(35);
                u.Property(x => x.HashedPassword).IsRequired(false).HasMaxLength(30);
                u.HasOne(x => x.Parent).WithMany().HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Cascade);
                u.Property(x => x.LastLogin).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
                u.Property(x => x.Status).IsRequired().HasDefaultValue(UserStatusEnum.NotVerified);
                u.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
                u.Property(x => x.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
                u.Property(x => x.Token).IsRequired(false).HasMaxLength(1000);
                u.Property(x => x.TokenExpires).IsRequired(false).HasDefaultValueSql("CURRENT_TIMESTAMP");
                u.Property(x => x.RefreshToken).IsRequired(false).HasMaxLength(1000);
                u.Property(x => x.RefreshTokenExpires).IsRequired(false).HasDefaultValueSql("CURRENT_TIMESTAMP");



            });
            builder.Entity<TutorProfile>(e => {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.Title).IsRequired().HasMaxLength(100);
                e.Property(x => x.Content).IsRequired().HasMaxLength(500);
                e.Property(x => x.Status).IsRequired().HasDefaultValue(TutorProfileEnum.Draft);
                e.Property(x => x.Meta).IsRequired(false).HasMaxLength(1000);
                e.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
                e.Property(x => x.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
            builder.Entity<TransactionHistory>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Amount).IsRequired();
                e.Property(x => x.Message).IsRequired().HasMaxLength(1000);
                e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
            builder.Entity<Schedule>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.DayOfWeek).IsRequired().HasMaxLength(7);
                e.Property(x => x.StartTime).IsRequired().HasDefaultValueSql("CURRENT_TIME");
                e.Property(x => x.EndTime).IsRequired().HasDefaultValueSql("CURRENT_TIME");
                e.Property(x => x.Room).IsRequired(false).HasMaxLength(200);
                e.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.StartDate).IsRequired().HasDefaultValueSql("CURRENT_DATE");
                e.Property(x => x.EndDate).IsRequired().HasDefaultValueSql("CURRENT_DATE");
                e.Property(x => x.Status).IsRequired().HasDefaultValue(ScheduleStatusEnum.InActive);
                e.HasOne(x => x.Student).WithMany().HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.SlotQuantity).IsRequired();
                e.HasOne(x => x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
                e.Property(x => x.UpdatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
            builder.Entity<Review>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.Title).IsRequired().HasMaxLength(100);
                e.Property(x => x.Content).IsRequired().HasMaxLength(100);
                e.Property(x => x.Rating).IsRequired();
                e.Property(x => x.Active).IsRequired().HasDefaultValue(false);
                e.HasOne(x => x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

            });
            builder.Entity<OrderCourse>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Order).WithMany().HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.Discount).IsRequired().HasDefaultValue(0);
                e.Property(x => x.Price).IsRequired();    
            });
            builder.Entity<Order>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.TotalAmount).IsRequired();
                e.Property(x => x.Status).IsRequired().HasDefaultValue(OrderEnum.Pending);
                e.Property(x => x.PaymentMethod).IsRequired(false);
                e.HasOne(x => x.CreatedUser).WithMany().HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

            });
            builder.Entity<CourseCategory>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Course>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.Property(x => x.Description).IsRequired().HasMaxLength(300);
                e.Property(x => x.Price).IsRequired();
                e.Property(x => x.Discount).IsRequired().HasDefaultValue(0);
                e.Property(x => x.Status).IsRequired().HasDefaultValue(CourseStatusEnum.Draft);
                e.Property(x => x.CourseDetail).IsRequired(false).HasMaxLength(500);
                e.Property(x => x.Thumbnail).IsRequired(false).HasMaxLength(1000);
                e.Property(x => x.Metadata).IsRequired(false).HasMaxLength(1000);
                e.Property(x => x.AvgRating).IsRequired().HasDefaultValue(0);
            });
            builder.Entity<Category>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
                e.Property(x => x.Description).IsRequired().HasMaxLength(300);
                e.Property(x => x.ImgUrl).IsRequired(false).HasMaxLength(1000);
                e.Property(x => x.Active).IsRequired().HasDefaultValue(false);
                e.HasOne(x => x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
            builder.Entity<BoughtCourse>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Child).WithMany().HasForeignKey(x => x.ChildId).OnDelete(DeleteBehavior.Cascade);
            });
        }

        public static string GetConnectionString(string connectionStringName)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString(connectionStringName);
            return connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(GetConnectionString("DefaultConnection"));
            }
        }
    }
}
