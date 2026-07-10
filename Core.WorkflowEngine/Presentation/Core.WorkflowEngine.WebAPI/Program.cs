using Core.WorkflowEngine.Application.Features.Configurations;
using Core.WorkflowEngine.Application.Features.Mappings.Configurations;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.Services;
using Core.WorkflowEngine.Persistence.CacheProvider;
using Core.WorkflowEngine.Persistence.Context;
using Core.WorkflowEngine.Persistence.Repositories;
using Core.WorkflowEngine.Persistence.UnitOfWork;
using Core.WorkflowEngine.WebAPI.Configurations;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "IdentityServerAccessToken";
    options.DefaultChallengeScheme = "IdentityServerAccessToken";
})
    .AddJwtBearer("IdentityServerAccessToken", options =>
    {
        options.Authority = "http://Core_IdentityServer_API:8080";
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
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" // [Authorize(Roles="...")] için önemli.
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                // Docker loglarında hatayı görüntülemek için.
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError($"Authentication Error : {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
    });

// Serilog Configurations
var logPath = "/app/logs/workflowengine";
Directory.CreateDirectory(logPath);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .WriteTo.Debug()
    .WriteTo.File(
        Path.Combine(logPath, "log-.txt"),
        rollingInterval: RollingInterval.Day)
    .CreateLogger();


builder.Host.UseSerilog();

builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

// Postgre SQL Configuration
builder.Services.AddDbContext<DBContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DBConnectionSettings"))
    );

// Repository Configuration
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// UnitOfWork Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Redis Configuration
var redisConnection = builder.Configuration.GetConnectionString("RedisConnectionSettings");
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    return ConnectionMultiplexer.Connect(redisConnection);
});
builder.Services.AddScoped<IDatabase>(sp =>
{
    var multiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
    return multiplexer.GetDatabase();
});
builder.Services.AddScoped(typeof(ICacheProvider), typeof(CacheProvider));

// AutoMApper Registration
builder.Services.AddAutoMapperServiceRegistration();

// MediatR Registration
builder.Services.AddMediatorServiceRegistration();

// Business Rule Configuration
builder.Services.AddBusinessRulesRegistration();

// Service ( used in handler classes ) Configuration
builder.Services.AddServiceRegistartion();

builder.Services.AddValidatorsFromAssembly(typeof(ValidatorAssemblyMarker).Assembly);

builder.Services.AddHelperServiceConfiguration();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Dogukan",
        Version = "Version 1.0.0"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT token giriniz."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Workflow Engine is starting...");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal($"An error occured while workflow engine starting. Error Message: {ex}");
}
finally
{
    Log.CloseAndFlush();
}
