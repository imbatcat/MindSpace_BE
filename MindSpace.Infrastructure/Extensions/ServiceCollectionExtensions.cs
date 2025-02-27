namespace MindSpace.Infrastructure.Extensions;

using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.Authentication;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Application.Interfaces.Services.EmailServices;
using MindSpace.Application.Interfaces.Services.FileReaderServices;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using MindSpace.Application.Interfaces.Services.SignalR;
using MindSpace.Application.Interfaces.Utilities;
using MindSpace.Application.Interfaces.Utilities.Seeding;
using MindSpace.Infrastructure.Persistence;
using MindSpace.Infrastructure.Repositories;
using MindSpace.Infrastructure.Seeders;
using MindSpace.Infrastructure.Services;
using MindSpace.Infrastructure.Services.AuthenticationServices;
using MindSpace.Infrastructure.Services.EmailServices;
using MindSpace.Infrastructure.Services.FileReaderServices;
using MindSpace.Infrastructure.Services.PaymentServices;
using MindSpace.Infrastructure.Services.SignalR;
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

        // Add SignalR for Readltime Communications
        services.AddSignalR();

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

        // Add SignalR Notification Service
        services.AddScoped<ISignalRNotification, SignalRNotificationService>();

        // Add HttpContextAccessor
        services.AddHttpContextAccessor();

        // Add Authentication Services 
        services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();
        services.AddScoped<IIDTokenProvider, IdTokenProvider>();
        services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();
        services.AddScoped<IUserContext, UserContext>();

        // Add Services
        services.AddScoped<IPaymentService, PayOSPaymentService>();
        services.AddScoped<IStripePaymentService, StripePaymentService>();
        services.AddScoped<IApplicationUserService, ApplicationUserRepository>();
        services.AddScoped<IResourcesService, ResourcesService>();
        services.AddSingleton<IExcelReaderService, ExcelReaderService>();
        services.AddScoped<ITestDraftService, TestDraftService>();
        services.AddScoped<IBlogDraftService, BlogDraftService>();
        services.AddScoped<ITestImportService, TestImportService>();

        // Add Seeders
        services.AddScoped<IFileReader, FileReader>();
        services.AddScoped<IIdentitySeeder, IdentitySeeder>();
        services.AddScoped<IDataCleaner, DatabaseCleaner>();
        services.AddScoped<ApplicationDbContextSeeder>();

        // Add User Context
        services.AddScoped<IUserContext, UserContext>();
    }
}