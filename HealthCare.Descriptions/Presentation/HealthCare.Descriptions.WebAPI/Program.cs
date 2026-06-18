using HealthCare.Descriptions.Application.Features.MappingProfiles.Services;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Services;
using HealthCare.Descriptions.Persistance.Context;
using HealthCare.Descriptions.Persistance.MessageBus.Consumers;
using HealthCare.Descriptions.Persistance.MessageBus.Publishers;
using HealthCare.Descriptions.Persistance.Repositories;
using HealthCare.Descriptions.WebAPI.Controllers;
using HealthCare.Descriptions.WebAPI.Middlewares;
using HealthCare.Descriptions.WebAPI.Services.BackgroundServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // Uzun Þema Ýsimlerine Dönüþtürmez.

// Serilog Configurations
var logPath = "/app/logs/descriptions";
Directory.CreateDirectory(logPath); // klasör yoksa oluþtur

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .WriteTo.Debug()
    .WriteTo.File(
        Path.Combine(logPath, "log-.txt"),
        rollingInterval: RollingInterval.Day
    )
    .CreateLogger();

builder.Host.UseSerilog();

// API'larýn JWT ile Korunma Ayarlarý Yapýlacak.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "IdentityServerAccessToken";
    options.DefaultChallengeScheme = "IdentityServerAccessToken";
})
    .AddJwtBearer("IdentityServerAccessToken", options =>
    {
        options.Authority = "http://HealthCare_IdentityServer_API:8080";
        options.Audience = "HealthCareFullPermission";
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "http://loacalhost:5001",
            ValidateAudience = true,
            ValidAudience = "HealthCareFullPermission",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero, // Saat farký toleransý sýfýrlanýr.
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" // [Authorize(Roles="...")] için önemli.
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                // Docker loglarýnda hatayý görüntülemek için.
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError($"Authentication Error : {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// DBContetx Configuration
builder.Services.AddDbContext<HealthCareDescriptionsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbconnectionSettings"));
});

// Custom MediatR Configuration
builder.Services.AddMediatRCustomRegistration();

// Custom AutoMapper Configuration
builder.Services.AddAutoMapperCustomRegistration();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddSingleton(typeof(IUserModifiedEventConsumer), typeof(UserModifiedEventConsumer));
builder.Services.AddSingleton(typeof(IRabbitMQEventPublisher), typeof(RabbitMQEventPublisher));

builder.Services.AddHostedService<UserModifiedEventConsumerBackgroundService>();

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
