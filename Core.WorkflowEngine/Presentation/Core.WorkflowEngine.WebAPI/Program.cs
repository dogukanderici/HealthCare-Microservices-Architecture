using Core.WorkflowEngine.Application.Features.Commons;
using Core.WorkflowEngine.Application.Features.Commons.Behaviors;
using Core.WorkflowEngine.Application.Features.Configurations;
using Core.WorkflowEngine.Application.Features.Mappings.Configurations;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Services;
using Core.WorkflowEngine.Persistence.Context;
using Core.WorkflowEngine.Persistence.Repositories;
using Core.WorkflowEngine.Persistence.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

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

// Postgre SQL Configuration
builder.Services.AddDbContext<DBContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DBConnectionSettings"))
    );

// Repository Configuration
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// UnitOfWork Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AutoMApper Registration
builder.Services.AddAutoMapperServiceRegistration();

// MediatR Registration
builder.Services.AddMediatorServiceRegistration();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

// Business Rule Configuration
builder.Services.AddBusinessRulesRegistration();

// Service ( used in handler classes ) Configuration
builder.Services.AddServiceRegistartion();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

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
