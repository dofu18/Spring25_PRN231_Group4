using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InfrastructureLayer
{
    //public class TutoringKidDbContextFactory : IDesignTimeDbContextFactory<TutoringKidDbContext>
    //{
    //    public TutoringKidDbContext CreateDbContext(string[] args)
    //    {
    //        // Navigate up one level to locate the "Controller" project
    //        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Controller");

    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //            .SetBasePath(basePath)  // Ensure correct path
    //            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //            .Build();

    //        Console.WriteLine($"Using ConnectionString: {configuration.GetConnectionString("DefaultConnection")}");

    //        var connectionString = configuration.GetConnectionString("DefaultConnection"); // 🔥 Ensure correct key!

    //        var optionsBuilder = new DbContextOptionsBuilder<TutoringKidDbContext>();
    //        optionsBuilder.UseNpgsql(connectionString);

    //        return new TutoringKidDbContext(optionsBuilder.Options, configuration); // ✅ Pass `configuration`
    //    }
    //}
    public class TutoringKidDbContextFactory : IDbContextFactory<TutoringKidDbContext>
    {
        private readonly IConfiguration _configuration;

        public TutoringKidDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TutoringKidDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TutoringKidDbContext>();
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));

            return new TutoringKidDbContext(optionsBuilder.Options, _configuration);
        }
    }
}
