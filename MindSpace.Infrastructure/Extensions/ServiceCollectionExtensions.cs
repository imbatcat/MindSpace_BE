using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Application.Commons.Utilities;
using MindSpace.Application.Commons.Utilities.Seeding;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Infrastructure.Persistence;
using MindSpace.Infrastructure.Repositories;
using MindSpace.Infrastructure.Seeders;

namespace MindSpace.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                // Add SqlServer and ConnectionString
                var connectionString = configuration.GetConnectionString("MindSpaceDb");
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

                // Add Identity services (authentication with tokens and cookies) with role supports, using ApplicationDbContext as the data store for Identity
                services.AddIdentityApiEndpoints<ApplicationUser>()
                    .AddRoles<ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

                // Add Seeders
                services.AddScoped<IFileReader, FileReader>();
                services.AddScoped<IIdentitySeeder, IdentitySeeder>();
                services.AddScoped<IEntityOrderProvider, EntityOrderProvider>();
                services.AddScoped<IDataCleaner, DatabaseCleaner>();
                services.AddScoped<ApplicationDbContextSeeder>();

                // Add Unit Of Work
                services.AddScoped<IUnitOfWork, UnitOfWork>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
