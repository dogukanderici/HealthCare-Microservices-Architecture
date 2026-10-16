using FluentValidation;
using HealthCare.Descriptions.Application.Common.Settings;
using HealthCare.Descriptions.Application.Features.Mappings;
using HealthCare.Descriptions.Application.Features.Validations;
using HealthCare.Descriptions.Configuration.Extentions;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ValidationHelper;
using HealthCare.Descriptions.WebAPI.Common.Middlewares;
using HealthCare.Descriptions.WebAPI.Policies.DynamicPolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// JWT Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "IdentityServerAccessToken";
    options.DefaultChallengeScheme = "IdentityServerAccessToken";
}).AddJwtBearer("IdentityServerAccessToken", options =>
    {
        options.Authority = "http://Core_IdentityServer_API:8080";
        options.Audience = "HealthCareFullPermission";
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:5001",
            ValidateAudience = true,
            ValidAudience = "HealthCareFullPermission",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };

        // Docker loglarına düşmesi için eklendi.
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError($"Authentication Error: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
    });

// Dynamic Role-Based Authorization Configurations
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DynamicRolePolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new DynamicRoleRequirement());
    });
});

builder.Services.AddScoped<IAuthorizationHandler, DynamicRoleHandler>();

//Serilog Configuration
var logPath = "/app/logs/descriptions";
Directory.CreateDirectory(logPath);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.Debug()
    .WriteTo.File(
        Path.Combine(logPath, "log-.txt"),
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

// DB Configuration ( DBContext ve Repository )
builder.Services.AddDBConfiguration(builder.Configuration);

// AutoMapper Configuration
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperAssemblyMarker));

// Mediator Configuration
builder.Services.AddMediatorRegistration();

// Service Registration
builder.Services.AddServiceRegistration();

// Business Rule Registration
builder.Services.AddBusinessRules();

// Helpers Configuration
builder.Services.AddHelperConfiguration();

builder.Services.Configure<CursorTokenSettings>(
    builder.Configuration.GetSection("CursorTokenSettings"));

// RabbitMQ Option Configuration
builder.Services.AddRabbitMQConfiguration(builder.Configuration);

// Controller Response Configuration
builder.Services.AddScoped(typeof(IControllerHelper<>), typeof(ControllerHelper<>));
builder.Services.AddScoped(typeof(IValidationHelper<>), typeof(ValidationHelper<>));

builder.Services.AddValidatorsFromAssembly(typeof(ValidatorAssemblyMarker).Assembly);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Description Servcice",
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

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Description service starting...");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal($"An error occured while description service starting. Error Message: {ex}");
}
finally
{
    Log.CloseAndFlush();
}