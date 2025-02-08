using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Application.Services.AuthenticationServices;
using MindSpace.Application.UserContext;
using MindSpace.Domain.Services.Authentication;

namespace MindSpace.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplications(this IServiceCollection services)
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
            services.AddScoped<UserRegistrationService>();

            // Add Token Providers
            services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();
            services.AddScoped<IIDTokenProvider, IdTokenProvider>();
            services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();
            services.AddSingleton<IExcelReaderService, ExcelReaderService>();

            // Add HttpContextAccessor
            services.AddHttpContextAccessor();
        }
    }
}