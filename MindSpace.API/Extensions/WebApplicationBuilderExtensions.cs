using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MindSpace.API.Middlewares;
using Serilog;
using Stripe;
using System.Text;

namespace MindSpace.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            // Configure Stripe API Key
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

            // Add Controllers with Endpoints
            builder.Services.AddControllers();

            // Add Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAccessTokenSettings:Secret"]!)),
                    ValidIssuer = builder.Configuration["JwtAccessTokenSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtAccessTokenSettings:Audience"],
                    ClockSkew = TimeSpan.FromSeconds(3) //Validator will still consider the token validate if it has expired 3 seconds ago, this is to prevent in case the request takes some time to reach to the api
                };
            });

            // Add Authorization
            builder.Services.AddAuthorization(options =>
            {
                var requireAuthPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.DefaultPolicy = requireAuthPolicy;
            });

            // Config Routing on Urls
            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "jwtToken_Auth",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization: Bearer",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT here"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:3000",
                        "http://localhost:5021",
                        "http://localhost:5174",
                        "http://localhost:5173",
                        "https://localhost:5021",
                        "https://localhost:3000",
                        "http://localhost:8081",
                        "http://192.168.1.2:19000",
                        "https://usefully-electric-termite.ngrok-free.app",
                        "https://seemingly-expert-macaque.ngrok-free.app",
                        "https://pleased-asp-kindly.ngrok-free.app"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithExposedHeaders("Location");
                });

                options.AddPolicy("AllowGemini", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("Content-Disposition");
                });
            });


            // tell swagger to support minimal apis, which the Identity apis are.
            builder.Services.AddEndpointsApiExplorer();

            // Add Custom middlewares
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<TimeLoggingMiddleware>();

            // Add Serilogs
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });

            // Add API Versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(

                    new UrlSegmentApiVersionReader(),
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("X-Version"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}