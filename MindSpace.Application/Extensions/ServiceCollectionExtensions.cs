using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MindSpace.Application.UserContext;

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

            // Add HttpContextAccessor
            services.AddHttpContextAccessor();
        }
    }
}
