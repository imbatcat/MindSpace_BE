using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Application.Services;
using MindSpace.Application.Services.AuthenticationServices;
using MindSpace.Application.UserContext;
using MindSpace.Domain.Interfaces.Services;
using MindSpace.Domain.Interfaces.Services.Authentication;

namespace MindSpace.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplications(this IServiceCollection services, IConfiguration configuration)
        {
            // Application assembly
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

            // CQRS MediaR
            services.AddMediatR(mtr => mtr.RegisterServicesFromAssembly(applicationAssembly));

            // Add AutoMapper
            services.AddAutoMapper(applicationAssembly);

            // Add Fluent Validation
            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

            // Add User Context
            services.AddScoped<IUserContext, UserContext.UserContext>();

            //Add email services
            services.AddFluentEmail(configuration["Email:Sender"])
                .AddSmtpSender(configuration["Email:Host"], int.Parse(configuration["Email:Port"]!), configuration["Email:Sender"], configuration["Email:Password"]);
            services.AddTransient<IEmailService, EmailSenderService>();

            // Add Token Providers
            services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();
            services.AddScoped<IIDTokenProvider, IdTokenProvider>();
            services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();

            //Add payment services
            services.AddScoped<IPaymentService, PaymentService>();

            // Add HttpContextAccessor
            services.AddHttpContextAccessor();

            // Add Services
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IResourcesService, ResourcesService>();
            services.AddSingleton<IExcelReaderService, ExcelReaderService>();
            services.AddScoped<ITestDraftService, TestPeriodicDraffService>();
            services.AddScoped<IBlogDraftService, BlogDraftService>();
            services.AddScoped<ITestImportService, TestImportService>();
        }
    }
}