namespace MindSpace.Infrastructure.Extensions;

using Application.Commons.Utilities;
using Application.Commons.Utilities.Seeding;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.Authentication;
using MindSpace.Infrastructure.Persistence;
using MindSpace.Infrastructure.Services;
using MindSpace.Infrastructure.Services.AuthenticationServices;
using Repositories;
using Seeders;
using StackExchange.Redis;

public static partial class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add SqlServer
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseAzureSql(configuration.GetConnectionString("MindSpaceDb"))
                .EnableSensitiveDataLogging());

        // Setup Redis
        services.AddSingleton<IConnectionMultiplexer>(config =>
        {
            var connString = configuration.GetConnectionString("RedisDb")
                ?? throw new Exception("Cannot get regis connection string");
            return ConnectionMultiplexer.Connect(connString);
        });

        // Add Identity services (authentication with tokens and cookies) with role supports, using ApplicationDbContext as the data store for Identity
        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Add Unit Of Work and Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Add email services
        services.AddFluentEmail(configuration["Email:Sender"])
            .AddSmtpSender(configuration["Email:Host"], int.Parse(configuration["Email:Port"]!), configuration["Email:Sender"], configuration["Email:Password"]);
        services.AddTransient<IEmailService, EmailSenderService>();

        // Add Token Providers
        services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();
        services.AddScoped<IIDTokenProvider, IdTokenProvider>();
        services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();

        // Add HttpContextAccessor
        services.AddHttpContextAccessor();

        // Add Services
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
        services.AddScoped<IResourcesService, ResourcesService>();
        services.AddSingleton<IExcelReaderService, ExcelReaderService>();
        services.AddScoped<ITestDraftService, TestDraftService>();
        services.AddScoped<IBlogDraftService, BlogDraftService>();
        services.AddScoped<ITestImportService, TestImportService>();

        // Add Seeders
        services.AddScoped<IFileReader, FileReader>();
        services.AddScoped<IIdentitySeeder, IdentitySeeder>();
        services.AddScoped<IEntityOrderProvider, EntityOrderProvider>();
        services.AddScoped<IDataCleaner, DatabaseCleaner>();
        services.AddScoped<ApplicationDbContextSeeder>();
    }
}