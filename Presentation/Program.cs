using Application.Extensions;
using Infrastructure.Extensions;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplications();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGroup("api/identity")
//    .WithTags("Identity")
//    .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
