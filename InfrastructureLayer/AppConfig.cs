using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfrastructureLayer.Repository.IRepository;
using InfrastructureLayer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer
{
    public static class AppConfig
    {
        public static IServiceCollection AddConfigService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<TutoringKidDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
