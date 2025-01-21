using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Infrastructure.Persistence;
using Restaurants.Application.Commons.Interfaces.Utilities;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeders;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("");
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString)
            //        .EnableSensitiveDataLogging());
            //services.AddIdentityApiEndpoints<>().AddEntityFrameworkStores<>();

            try
            {
                var connectionString = configuration.GetConnectionString("");
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

                // Add FileReader service
                services.AddScoped<IFileReader, FileReader>();

                // Set Services for Data Seeders
                services.AddScoped<ApplicationDbContextSeeder>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
