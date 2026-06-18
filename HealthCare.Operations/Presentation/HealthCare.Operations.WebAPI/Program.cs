using HealthCare.Operations.Application.Features.MappingProfiles.Service;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Application.Services;
using HealthCare.Operations.Persistence.Context;
using HealthCare.Operations.Persistence.Repositories;
using HealthCare.Operations.WebAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog Configurations
var logPath = "/app/logs/operations";
Directory.CreateDirectory(logPath);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.File(
        Path.Combine(logPath, "log-.txt"),
        rollingInterval: RollingInterval.Day
    )
    .CreateLogger();

builder.Host.UseSerilog();

// API'ların JWT ile Korunma Ayarları Yapılacak.

/*
 * JWT
 */

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// MediatR Configuration
builder.Services.AddMediatorCustomConfiguration();

// AutoMapper Configuration
builder.Services.AddAutoMapperConfiguration();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

builder.Services.AddDbContext<HealthCareOperationsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionSetting"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
