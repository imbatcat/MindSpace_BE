namespace MindSpace.Infrastructure.Extensions;

using Domain.Entities.Identity;
using FUNewsManagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Application.BackgroundJobs.MeetingRooms;
using MindSpace.Application.Interfaces.Repos;
using MindSpace.Application.Interfaces.Services;
using MindSpace.Application.Interfaces.Services.AuthenticationServices;
using MindSpace.Application.Interfaces.Services.EmailServices;
using MindSpace.Application.Interfaces.Services.FileReaderServices;
using MindSpace.Application.Interfaces.Services.ImageServices;
using MindSpace.Application.Interfaces.Services.PaymentServices;
using MindSpace.Application.Interfaces.Utilities;
using MindSpace.Application.Interfaces.Utilities.Seeding;
using MindSpace.Infrastructure.Persistence;
using MindSpace.Infrastructure.Repositories;
using MindSpace.Infrastructure.Seeders;
using MindSpace.Infrastructure.Services;
using MindSpace.Infrastructure.Services.AuthenticationServices;
using MindSpace.Infrastructure.Services.BackgroundServices;
using MindSpace.Infrastructure.Services.CachingServices;
using MindSpace.Infrastructure.Services.ChatServices;
using MindSpace.Infrastructure.Services.EmailServices;
using MindSpace.Infrastructure.Services.FileReaderServices;
using MindSpace.Infrastructure.Services.PaymentServices;
using MindSpace.Infrastructure.Services.SignalR;
using Quartz;
using StackExchange.Redis;
using System;

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
                ?? throw new Exception("Cannot get redis connection string");
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

        // Add HttpContextAccessor
        services.AddHttpContextAccessor();

        // Add SignalR Services
        services.AddSignalR();
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddSingleton<IPaymentNotificationService, PaymentNotificationService>();

        // Add Quartz services
        services.AddQuartz(q =>
        {
            q.UseSimpleTypeLoader();      // Load all jobs in current assembly
            q.UseInMemoryStore();         // Use in-memory storage for job and trigger
            q.UseDefaultThreadPool(tp =>
            {
                tp.MaxConcurrency = 10;    // Set max concurrency to 10
            });

            // TO BE REFACTORED
            // Schedule removing meeting rooms 
            q.AddJob<ExpireMeetingRoomJob>(opts => opts.WithIdentity("ExpireMeetingRoomJob"))
                .AddTrigger(opts => opts
                    .ForJob("ExpireMeetingRoomJob")
                    .WithIdentity("ExpireMeetingRoomJobTrigger")
                    .WithCronSchedule("59 59 23 * * ?")); // Runs every day at 23:59:59
        });

        // Add the Quartz.NET hosted service
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        services.AddScoped<IBackgroundJobService, BackgroundJobService>();

        // Add Authentication Services 
        services.AddScoped<IUserTokenService, UserTokenService>();
        services.AddScoped<IUserContext, UserContext>();

        // Add HttpClient Services
        services.AddHttpClient();

        // Add Image Service
        services.AddSingleton<IImageService, ImageService>();

        // Add Chat Agent Service
        services.AddSingleton<IAgentChatService, GeminiAgentChatService>();

        // Add Caching Service
        services.AddSingleton<IResponseCachingService, ResponseCachingService>();

        // Add Caching Service
        services.AddSingleton<IResponseCachingService, ResponseCachingService>();

        // Add Payment Services
        services.AddScoped<IPaymentService, PayOSPaymentService>();
        services.AddScoped<IStripePaymentService, StripePaymentService>();

        services.AddScoped(typeof(UserManager<>));
        services.AddScoped(typeof(IApplicationUserService<>), typeof(ApplicationUserService<>));
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
    }
}