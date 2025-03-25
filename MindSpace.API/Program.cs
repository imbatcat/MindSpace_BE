using Microsoft.EntityFrameworkCore;
using MindSpace.API.Extensions;
using MindSpace.API.Middlewares;
using MindSpace.Application.Extensions;
using MindSpace.Application.Interfaces.Utilities.Seeding;
using MindSpace.Infrastructure.Extensions;
using MindSpace.Infrastructure.Persistence;
using MindSpace.Infrastructure.Services.SignalR;

var builder = WebApplication.CreateBuilder(args);

// ====================================
// === Add services to the container
// ====================================

builder.AddPresentation(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplications(builder.Configuration);

// ====================================
// === Build the application
// ====================================

var app = builder.Build();

// ====================================
// === Use Middlewares
// ====================================

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<TimeLoggingMiddleware>();

// Configure CORS
app.UseCors("AllowFrontend");
app.UseCors("AllowGemini");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adds identity api endpoints
app.MapGroup("api/identities")
    .WithTags("Identities");

app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificationHub>("/hub/notifications");
app.MapHub<WebRTCHub>("/hub/webrtc");
app.MapHub<PaymentHub>("/hub/payment");
app.MapFallbackToController("Index", "Fallback");

// ===================================================
// === Create a scope and call the service manually
// ===================================================

using var scope = app.Services.CreateScope();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
var applicationSeeder = scope.ServiceProvider.GetRequiredService<ApplicationDbContextSeeder>();
var dataCleaner = scope.ServiceProvider.GetRequiredService<IDataCleaner>();
var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

var IsClearAndReseedData = app.Configuration.GetValue<bool>("ClearAndReseedData");

if (IsClearAndReseedData)
{
    try
    {
        dataCleaner.ClearData();
        await applicationSeeder.SeedAllAsync();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error happens during data seeding!");
    }
}

try
{
    await applicationDbContext.Database.MigrateAsync();
}
catch (Exception ex)
{
    logger.LogError(ex, "Error happens during migrations!");
}

app.Run();
