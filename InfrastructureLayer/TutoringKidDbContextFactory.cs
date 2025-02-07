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
    public class TutoringKidDbContextFactory : IDesignTimeDbContextFactory<TutoringKidDbContext>
    {
        public TutoringKidDbContext CreateDbContext(string[] args)
        {
            // Navigate up one level to locate the "Controller" project
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Controller");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)  // Ensure correct path
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection"); // 🔥 Ensure correct key!

            var optionsBuilder = new DbContextOptionsBuilder<TutoringKidDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new TutoringKidDbContext(optionsBuilder.Options, configuration); // ✅ Pass `configuration`
        }
    }
}
