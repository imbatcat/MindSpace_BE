using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("");
            //services.AddDbContext<>(options => options.UseSqlServer(connectionString)
            //        .EnableSensitiveDataLogging());
            //services.AddIdentityApiEndpoints<>().AddEntityFrameworkStores<>();
        }
    }
}
