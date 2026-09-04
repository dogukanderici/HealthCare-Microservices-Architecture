using HealthCare.Descriptions.Application.Features.Mappings;
using HealthCare.Descriptions.Configuration.Extentions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Serilog Configuration
var logPath = "/app/logs/descriptions";
Directory.CreateDirectory(logPath);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.File(
        Path.Combine(logPath, "log-.txt"),
        rollingInterval: RollingInterval.Day)
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

// Service Registration
builder.Services.AddServiceRegistration();

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